using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;

namespace Microsoft.OpenApi.Models
{

    public class OpenApiSchemaReference : OpenApiSchema
    {
        private OpenApiSchema _target;
        private readonly OpenApiDocument _hostDocument;

        public OpenApiSchemaReference(OpenApiDocument hostDocument)
        {
            this._hostDocument = hostDocument;
        }

        private OpenApiSchema Target { get { 
                
                if (_target == null)
                {
                    _target = _hostDocument.ResolveReferenceTo<OpenApiSchema>(this.Reference);
                }
                return _target; 
            } }

        public override string Title { get => Target.Title; set => Target.Title = value; }
        public override string Type { get => Target.Type; set => Target.Type = value; }
        public override string Format { get => Target.Format; set => Target.Format = value; }
        public override string Description { get => Target.Description; set => Target.Description = value; }
        public override decimal? Maximum { get => Target.Maximum; set => Target.Maximum = value; }
        public override bool? ExclusiveMaximum { get => Target.ExclusiveMaximum; set => Target.ExclusiveMaximum = value; }
        public override decimal? Minimum { get => Target.Minimum; set => Target.Minimum = value; }
        public override bool? ExclusiveMinimum { get => Target.ExclusiveMinimum; set => Target.ExclusiveMinimum = value; }
        public override int? MaxLength { get => Target.MaxLength; set => Target.MaxLength = value; }
        public override int? MinLength { get => Target.MinLength; set => Target.MinLength = value; }
        public override string Pattern { get => Target.Pattern; set => Target.Pattern = value; }
        public override decimal? MultipleOf { get => Target.MultipleOf; set => Target.MultipleOf = value; }
        public override IOpenApiAny Default { get => Target.Default; set => Target.Default = value; }
        public override bool ReadOnly { get => Target.ReadOnly; set => Target.ReadOnly = value; }
        public override bool WriteOnly { get => Target.WriteOnly; set => Target.WriteOnly = value; }
        public override IList<OpenApiSchema> AllOf { get => Target.AllOf; set => Target.AllOf = value; }
        public override IList<OpenApiSchema> OneOf { get => Target.OneOf; set => Target.OneOf = value; }
        public override IList<OpenApiSchema> AnyOf { get => Target.AnyOf; set => Target.AnyOf = value; }
        public override OpenApiSchema Not { get => Target.Not; set => Target.Not = value; }
        public override ISet<string> Required { get => Target.Required; set => Target.Required = value; }
        public override OpenApiSchema Items { get => Target.Items; set => Target.Items = value; }
        public override int? MaxItems { get => Target.MaxItems; set => Target.MaxItems = value; }
        public override int? MinItems { get => Target.MinItems; set => Target.MinItems = value; }
        public override bool? UniqueItems { get => Target.UniqueItems; set => Target.UniqueItems = value; }
        public override IDictionary<string, OpenApiSchema> Properties { get => Target.Properties; set => Target.Properties = value; }
        public override int? MaxProperties { get => Target.MaxProperties; set => Target.MaxProperties = value; }
        public override int? MinProperties { get => Target.MinProperties; set => Target.MinProperties = value; }
        public override bool AdditionalPropertiesAllowed { get => Target.AdditionalPropertiesAllowed; set => Target.AdditionalPropertiesAllowed = value; }
        public override OpenApiSchema AdditionalProperties { get => Target.AdditionalProperties; set => Target.AdditionalProperties = value; }
        public override OpenApiDiscriminator Discriminator { get => Target.Discriminator; set => Target.Discriminator = value; }
        public override IOpenApiAny Example { get => Target.Example; set => Target.Example = value; }
        public override IList<IOpenApiAny> Enum { get => Target.Enum; set => Target.Enum = value; }
        public override bool Nullable { get => Target.Nullable; set => Target.Nullable = value; }
        public override OpenApiExternalDocs ExternalDocs { get => Target.ExternalDocs; set => Target.ExternalDocs = value; }
        public override bool Deprecated { get => Target.Deprecated; set => Target.Deprecated = value; }
        public override OpenApiXml Xml { get => Target.Xml; set => Target.Xml = value; }
        public override IDictionary<string, IOpenApiExtension> Extensions { get => Target.Extensions; set => Target.Extensions = value; }
        public override bool UnresolvedReference { get => base.UnresolvedReference; set => base.UnresolvedReference = value; }
        public override OpenApiReference Reference { 
            get => base.Reference; 
            set {
                base.Reference = value;
                _target = null;
            }
         }
    }
}
