using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Tests.Common.Builders.Interfaces;

namespace Umbraco.Tests.Common.Builders
{
    public class DictionaryItemBuilder
        : BuilderBase<DictionaryItem>,
            IWithIdBuilder,
            IWithCreateDateBuilder,
            IWithUpdateDateBuilder,
            IWithDeleteDateBuilder,
            IWithKeyBuilder
    {
        private readonly List<DictionaryTranslationBuilder> _translationBuilders =
            new List<DictionaryTranslationBuilder>();

        private DateTime? _createDate;
        private DateTime? _deleteDate;
        private int? _id;
        private string _itemKey;
        private Guid? _key;
        private Guid? _parentId;
        private DateTime? _updateDate;

        DateTime? IWithCreateDateBuilder.CreateDate
        {
            get => _createDate;
            set => _createDate = value;
        }

        DateTime? IWithDeleteDateBuilder.DeleteDate
        {
            get => _deleteDate;
            set => _deleteDate = value;
        }

        int? IWithIdBuilder.Id
        {
            get => _id;
            set => _id = value;
        }

        Guid? IWithKeyBuilder.Key
        {
            get => _key;
            set => _key = value;
        }

        DateTime? IWithUpdateDateBuilder.UpdateDate
        {
            get => _updateDate;
            set => _updateDate = value;
        }

        public override DictionaryItem Build()
        {
            var createDate = _createDate ?? DateTime.Now;
            var updateDate = _updateDate ?? DateTime.Now;
            var deleteDate = _deleteDate ?? null;
            var id = _id ?? 1;
            var key = _key ?? Guid.NewGuid();
            var parentId = _parentId ?? null;
            var itemKey = _itemKey ?? Guid.NewGuid().ToString();

            Reset();
            var result = new DictionaryItem(itemKey)
            {
                Translations = _translationBuilders.Select(x => x.Build()),
                CreateDate = createDate,
                UpdateDate = updateDate,
                DeleteDate = deleteDate,
                Id = id,
                ParentId = parentId,
                Key = key,
            };
            return result;
        }

        protected override void Reset()
        {
            _createDate = null;
            _deleteDate = null;
            _id = null;
            _itemKey = null;
            _key = null;
            _parentId = null;
            _updateDate = null;
        }

        public DictionaryItemBuilder WithParentId(Guid parentId)
        {
            _parentId = parentId;
            return this;
        }

        public DictionaryItemBuilder WithItemKey(string itemKey)
        {
            _itemKey = itemKey;
            return this;
        }

        public DictionaryTranslationBuilder AddTranslation()
        {
            var builder = new DictionaryTranslationBuilder(this);

            _translationBuilders.Add(builder);

            return builder;
        }

        public DictionaryItemBuilder WithRandomTranslations(int count)
        {
            for (var i = 0; i < count; i++)
            {
                AddTranslation().Done();
            }

            return this;
        }
    }
}
