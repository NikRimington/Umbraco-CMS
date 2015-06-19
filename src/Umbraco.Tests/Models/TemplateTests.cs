using System;
using NUnit.Framework;
using Umbraco.Core.Models;
using Umbraco.Core.Serialization;
using Umbraco.Tests.TestHelpers;

namespace Umbraco.Tests.Models
{
    [TestFixture]
    public class TemplateTests : BaseUmbracoConfigurationTest
    {
        [Test]
        public void Can_Deep_Clone()
        {
            var item = new Template("Test", "test")
            {
                Id = 3,
                CreateDate = DateTime.Now,                
                Key = Guid.NewGuid(),
                UpdateDate = DateTime.Now,
                Content = "blah",
                Path = "-1,3",
                IsMasterTemplate = true,                
                MasterTemplateAlias = "master",
                MasterTemplateId = new Lazy<int>(() => 88)                
            };

            var clone = (Template)item.DeepClone();

            Assert.AreNotSame(clone, item);
            Assert.AreEqual(clone, item);
            Assert.AreEqual(clone.Path, item.Path);
            Assert.AreEqual(clone.IsMasterTemplate, item.IsMasterTemplate);
            Assert.AreEqual(clone.CreateDate, item.CreateDate);
            Assert.AreEqual(clone.Alias, item.Alias);
            Assert.AreEqual(clone.Id, item.Id);
            Assert.AreEqual(clone.Key, item.Key);
            Assert.AreEqual(clone.MasterTemplateAlias, item.MasterTemplateAlias);
            Assert.AreEqual(clone.MasterTemplateId.Value, item.MasterTemplateId.Value);
            Assert.AreEqual(clone.Name, item.Name);
            Assert.AreEqual(clone.UpdateDate, item.UpdateDate);

            //This double verifies by reflection
            var allProps = clone.GetType().GetProperties();
            foreach (var propertyInfo in allProps)
            {
                Assert.AreEqual(propertyInfo.GetValue(clone, null), propertyInfo.GetValue(item, null));
            }
        }

        [Test]
        public void Can_Serialize_Without_Error()
        {
            var ss = new SerializationService(new JsonNetSerializer());

            var item = new Template("Test", "test")
            {
                Id = 3,
                CreateDate = DateTime.Now,
                Key = Guid.NewGuid(),
                UpdateDate = DateTime.Now,
                Content = "blah",
                MasterTemplateAlias = "master",
                MasterTemplateId = new Lazy<int>(() => 88)
            };

            var result = ss.ToStream(item);
            var json = result.ResultStream.ToJsonString();
            Console.WriteLine(json);
        }

    }
}