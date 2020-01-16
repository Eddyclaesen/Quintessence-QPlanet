using System.Collections.Generic;
using System.Data.Entity;
using Moq;
using Quintessence.CulturalFit.DataModel;
using Quintessence.CulturalFit.Data.Interfaces;

namespace Quintessence.CulturalFit.Business.Tests.Base
{
    //public class QContext : IQContext
    //{
    //    private Mock<IQContext> _context;

    //    public QContext()
    //    {
    //        _context = new Mock<IQContext>();
    //        _context.Setup(q => q.TheoremLists).Returns(CreateTheoremListData());
    //    }

    //    private MockDbSet<TheoremList> CreateTheoremListData()
    //    {
    //        var theoremLists = new List<TheoremList>
    //                                     {
    //                                         new TheoremList{Id=1, IsCompleted=true, Theorems=CreateListOfTheorem(), TheoremVersionId=1}
    //                                     };

    //        return new MockDbSet<TheoremList>(theoremLists);
    //    }

    //    private List<TheoremTranslation> CreateListOfTheoremTranslation()
    //    {
    //        var theoremTranslations = new List<TheoremTranslation>
    //                                      {
    //                                          new TheoremTranslation
    //                                              {
    //                                                  Id = 1,
    //                                                  LanguageId = 1,
    //                                                  Translation = "language1",
    //                                                  TranslationGroup = 1
    //                                              },
    //                                          new TheoremTranslation
    //                                              {Id = 2, LanguageId = 2, Translation = "taal1", TranslationGroup = 1},
    //                                          new TheoremTranslation
    //                                              {
    //                                                  Id = 3,
    //                                                  LanguageId = 3,
    //                                                  Translation = "langue2",
    //                                                  TranslationGroup = 2
    //                                              },
    //                                              new TheoremTranslation
    //                                              {
    //                                                  Id = 4,
    //                                                  LanguageId = 1,
    //                                                  Translation = "language2",
    //                                                  TranslationGroup = 1
    //                                              },
    //                                          new TheoremTranslation
    //                                              {Id = 5, LanguageId = 2, Translation = "taal2", TranslationGroup = 2},
    //                                          new TheoremTranslation
    //                                              {
    //                                                  Id = 6,
    //                                                  LanguageId = 3,
    //                                                  Translation = "langue2",
    //                                                  TranslationGroup = 2
    //                                              }
    //                                      };
    //        return theoremTranslations;
    //    }

    //    private List<Theorem> CreateListOfTheorem()
    //    {
    //        var theorems = new List<Theorem>
    //                           {
    //                               new Theorem
    //                                   {
    //                                       Id = 1,
    //                                       TheoremTranslations = CreateListOfTheoremTranslation(),
    //                                       TranslationGroup = 1
    //                                   },
    //                               new Theorem
    //                                   {
    //                                       Id = 2,
    //                                       TheoremTranslations = CreateListOfTheoremTranslation(),
    //                                       TranslationGroup = 2
    //                                   }
    //                           };
    //        return theorems;
    //    }

    //    public void Dispose()
    //    {
    //    }

    //    public IDbSet<TheoremTranslation> TheoremTranslations
    //    {
    //        get { return _context.Object.TheoremTranslations; }
    //        set { _context.Object.TheoremTranslations = value; }
    //    }

    //    public IDbSet<Theorem> Theorems
    //    {
    //        get { return _context.Object.Theorems; }
    //        set { _context.Object.Theorems = value; }
    //    }

    //    public IDbSet<TheoremList> TheoremLists
    //    {
    //        get { return _context.Object.TheoremLists; }
    //        set { _context.Object.TheoremLists = value; }
    //    }

    //    public IDbSet<QuestionedTheorem> QuestionedTheorems
    //    {
    //        get { return _context.Object.QuestionedTheorems; }
    //        set { _context.Object.QuestionedTheorems = value; }
    //    }

    //}

    //public class QContext : IQContext
    //{
    //    private Mock<QContext> _context;

    //    public QContext()
    //    {
    //        _context = new Mock<QContext>();
    //        _context.Setup(q => q.QuestionedTheorems).Returns(CreateQuestionedTheorems());
    //    }

    //    private IDbSet<QuestionedTheorem> CreateQuestionedTheorems()
    //    {
    //        var questionedTheorems = new List<QuestionedTheorem>
    //                                     {
    //                                         new QuestionedTheorem{Id = 1, IsLeastApplicable = false, IsMostApplicable = true, TheoremId = 2, Theorem = new Theorem{Id =2}},
    //                                         new QuestionedTheorem{},
    //                                         new QuestionedTheorem()
    //                                     };

    //        return new MockDbSet<QuestionedTheorem>(questionedTheorems);
    //    }

    //    public void Dispose()
    //    {
    //    }

    //    public IDbSet<TheoremTranslation> TheoremTranslations { get; set; }

    //    public IDbSet<Theorem> Theorems { get; set; }

    //    public IDbSet<TheoremList> TheoremLists { get; set; }

    //    public IDbSet<QuestionedTheorem> QuestionedTheorems { get; set; }
    //}
}
