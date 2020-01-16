using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quintessence.CulturalFit.Data.Tests.Base;
using Quintessence.CulturalFit.Infra.Data;

namespace Quintessence.CulturalFit.Data.Tests
{
    [TestClass]
    public class QContextTests : BaseData
    {
        //[TestMethod]
        public void TestCreateTheoremListTemplate()
        {
            try
            {
                using (var context = CreateContext())
                {
                    var template = context.TheoremListTemplates.CreateEntity();

                    template.Name = "Template";

                    context.TheoremListTemplates.Add(template);

                    context.SaveChanges();

                    template = (from tlt in context.TheoremListTemplates
                                where tlt.Id == template.Id
                                select tlt).SingleOrDefault();

                    Assert.IsNotNull(template);
                }
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        //[TestMethod]
        public void TestCreateTheoremListTemplateWithTheorems()
        {
            try
            {
                using (var context = CreateContext())
                {
                    var template = context.TheoremListTemplates.CreateEntity();
                    context.TheoremListTemplates.Add(template);

                    template.Name = "Template";

                    template.AddTheoremTemplate(context.TheoremTemplates.CreateEntity());

                    context.SaveChanges();

                    template = (from tlt in context.TheoremListTemplates.Include(t => t.TheoremTemplates)
                                where tlt.Id == template.Id
                                select tlt).SingleOrDefault();

                    Assert.IsNotNull(template);
                    Assert.IsNotNull(template.TheoremTemplates);
                    Assert.IsTrue(template.TheoremTemplates.Count > 0);
                }
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }
    }
}
