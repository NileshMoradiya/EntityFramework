// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Metadata.ModelConventions
{
    public class ConventionDispatcher
    {
        private readonly ConventionSet _conventionSet;

        public ConventionDispatcher([NotNull] ConventionSet conventionSet)
        {
            Check.NotNull(conventionSet, nameof(conventionSet));

            _conventionSet = conventionSet;
        }

        public virtual InternalEntityBuilder OnEntityTypeAdded([NotNull] InternalEntityBuilder entityBuilder)
        {
            Check.NotNull(entityBuilder, nameof(entityBuilder));

            foreach (var entityTypeConvention in _conventionSet.EntityTypeAddedConventions)
            {
                entityBuilder = entityTypeConvention.Apply(entityBuilder);
                if (entityBuilder == null)
                {
                    break;
                }
            }

            return entityBuilder;
        }

        public virtual InternalPropertyBuilder OnPropertyAdded([NotNull] InternalPropertyBuilder propertyBuilder)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));

            foreach (var entityTypeConvention in _conventionSet.PropertyAddedConventions)
            {
                propertyBuilder = entityTypeConvention.Apply(propertyBuilder);
                if (propertyBuilder == null)
                {
                    break;
                }
            }

            return propertyBuilder;
        }

        public virtual InternalKeyBuilder OnKeyAdded([NotNull] InternalKeyBuilder keyBuilder)
        {
            Check.NotNull(keyBuilder, nameof(keyBuilder));

            foreach (var keyConvention in _conventionSet.KeyAddedConventions)
            {
                keyBuilder = keyConvention.Apply(keyBuilder);
                if (keyBuilder == null)
                {
                    break;
                }
            }

            return keyBuilder;
        }

        public virtual void OnForeignKeyRemoved([NotNull] InternalEntityBuilder entityBuilder, [NotNull] ForeignKey foreignKey)
        {
            Check.NotNull(entityBuilder, nameof(entityBuilder));
            Check.NotNull(foreignKey, nameof(foreignKey));

            foreach (var foreignKeyConvention in _conventionSet.ForeignKeyRemovedConventions)
            {
                foreignKeyConvention.Apply(entityBuilder, foreignKey);
            }
        }

        public virtual InternalRelationshipBuilder OnForeignKeyAdded([NotNull] InternalRelationshipBuilder relationshipBuilder)
        {
            Check.NotNull(relationshipBuilder, nameof(relationshipBuilder));

            foreach (var relationshipConvention in _conventionSet.ForeignKeyAddedConventions)
            {
                relationshipBuilder = relationshipConvention.Apply(relationshipBuilder);
                if (relationshipBuilder == null)
                {
                    break;
                }
            }

            return relationshipBuilder;
        }

        public virtual InternalModelBuilder InitializingModel([NotNull] InternalModelBuilder modelBuilder)
        {
            Check.NotNull(modelBuilder, nameof(modelBuilder));

            foreach (var modelConvention in _conventionSet.ModelConventions)
            {
                modelBuilder = modelConvention.Apply(modelBuilder);
                if (modelBuilder == null)
                {
                    break;
                }
            }

            return modelBuilder;
        }
    }
}
