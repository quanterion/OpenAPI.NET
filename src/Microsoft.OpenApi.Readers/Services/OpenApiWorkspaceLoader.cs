using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers.Interface;
using Microsoft.OpenApi.Services;
using SharpYaml.Model;

namespace Microsoft.OpenApi.Readers.Services
{
    internal class OpenApiWorkspaceLoader 
    {
        private OpenApiWorkspace _workspace;
        private IStreamLoader _loader;
        private readonly OpenApiReaderSettings _readerSettings;

        public OpenApiWorkspaceLoader(OpenApiWorkspace workspace, IStreamLoader loader, OpenApiReaderSettings readerSettings)
        {
            _workspace = workspace;
            _loader = loader;
            _readerSettings = readerSettings;
        }

        internal async Task LoadAsync(OpenApiReference reference, OpenApiDocument document, OpenApiDiagnostic diagnostic)
        {
            _workspace.AddDocument(reference.ExternalResource, document);
            document.Workspace = _workspace;

            // Collect remote references by walking document
            var referenceCollector = new OpenApiRemoteReferenceCollector(document);
            var collectorWalker = new OpenApiWalker(referenceCollector);
            collectorWalker.Walk(document);

            var reader = new OpenApiStreamReader(_readerSettings);

            // Walk references
            foreach (var item in referenceCollector.References)
            {
                // If not already in workspace, load it and process references
                if (!_workspace.Contains(item.ExternalResource)) // TODO: This won't work for fragments because fragments need to be identified by the full path, not just the file. 
                {
                    var input = await _loader.LoadAsync(new Uri(item.ExternalResource, UriKind.RelativeOrAbsolute));
                    
                    if (item.IsFragment)
                    {
                        IOpenApiReferenceable fragment;
                        // TODO: This will only work if the external document only contains a single fragment
                        switch (item.Type) {
                            case ReferenceType.Schema:
                                fragment = reader.ReadFragment<OpenApiSchema>(input, diagnostic.SpecificationVersion, out diagnostic);
                                break;
                            case ReferenceType.Parameter:
                                fragment = reader.ReadFragment<OpenApiParameter>(input, diagnostic.SpecificationVersion, out diagnostic);
                                break;
                            case ReferenceType.Example:
                                fragment = reader.ReadFragment<OpenApiExample>(input, diagnostic.SpecificationVersion, out diagnostic);
                                break;
                            case ReferenceType.Callback:
                                fragment = reader.ReadFragment<OpenApiCallback>(input, diagnostic.SpecificationVersion, out diagnostic);
                                break;
                            case ReferenceType.Response:
                                fragment = reader.ReadFragment<OpenApiResponse>(input, diagnostic.SpecificationVersion, out diagnostic);
                                break;
                            case ReferenceType.RequestBody:
                                fragment = reader.ReadFragment<OpenApiRequestBody>(input, diagnostic.SpecificationVersion, out diagnostic);
                                break;
                            case ReferenceType.Header:
                                fragment = reader.ReadFragment<OpenApiHeader>(input, diagnostic.SpecificationVersion, out diagnostic);
                                break;
                            case ReferenceType.Link:
                                fragment = reader.ReadFragment<OpenApiLink>(input, diagnostic.SpecificationVersion, out diagnostic);
                                break;
                            default:
                                fragment = null;
                                break;      
                        }
                        
                        _workspace.AddFragment(item.ExternalResource, fragment);  // V2 and V3 fragment references are the same.
                    }
                    else
                    {
                        var result = await reader.ReadAsync(input); 
                        foreach(var error in result.OpenApiDiagnostic.Errors)
                        {
                            diagnostic.Errors.Add(error);
                        }
                        await LoadAsync(item, result.OpenApiDocument,diagnostic);
                    }
                }
            }
        }
    }
}
