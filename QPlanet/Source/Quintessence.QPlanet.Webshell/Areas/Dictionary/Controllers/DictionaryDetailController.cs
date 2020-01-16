using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Quintessence.QPlanet.ViewModel.Dim;

namespace Quintessence.QPlanet.Webshell.Areas.Dictionary.Controllers
{
    public class DictionaryDetailController : DictionaryController
    {
        public ActionResult Edit(Guid id)
        {
            try
            {
                var dictionaryView = Mapper.Map<DictionaryModel>(Execute(service => service.RetrieveDictionary(id)));

                return View(dictionaryView);
            }
            catch (Exception exception)
            {
                return HandleError(exception);
            }
        }

        [HttpPost]
        public ActionResult Edit(DictionaryModel dictionaryModel)
        {
            try
            {
                var dictionary = Execute(service => service.RetrieveDictionary(dictionaryModel.Id));

                if (dictionary == null)
                    dictionary = new QService.QueryModel.Dim.DictionaryView();

                Mapper.Map(dictionaryModel, dictionary);

                dictionary.Description = dictionaryModel.Description;
                dictionary.Name = dictionaryModel.Name;

                return RedirectToAction("Edit", new { id = dictionaryModel.Id });
            }
            catch (Exception exception)
            {
                return HandleError(exception);
            }
        }

        public ActionResult Clusters(Guid id)
        {
            try
            {
                var dictionaryView = Mapper.Map<DictionaryModel>(Execute(service => service.RetrieveDictionary(id)));

                return View(dictionaryView);
            }
            catch (Exception exception)
            {
                return HandleError(exception);
            }
        }

        public ActionResult DictionaryDetail(Guid id, int languageId)
        {
            try
            {
                //var dictionary = Execute(service => service.RetrieveDictionaryDetail(id, languageId));

                //var dictionaryModel = Mapper.Map<DictionaryModel>(dictionary);

                //dictionaryModel.DictionaryClusters =
                //    dictionary.DictionaryClusters
                //    .OrderBy(dcl => dcl.Name)
                //    .Select(dcl =>
                //                {
                //                    var dictionaryClusterModel = Mapper.Map<DictionaryClusterModel>(dcl);

                //                    var dictionaryClusterTranslation = dcl.DictionaryClusterTranslations.SingleOrDefault(dclt => dclt.LanguageId == languageId);
                //                    if (dictionaryClusterTranslation != null)
                //                        dictionaryClusterModel.Name = dictionaryClusterTranslation.Text;

                //                    dictionaryClusterModel.DictionaryCompetences = dcl.DictionaryCompetences
                //                        .OrderBy(dco => dco.Name)
                //                        .Select(dco =>
                //                                    {
                //                                        var dictionaryCompetenceModel = Mapper.Map<DictionaryCompetenceModel>(dco);

                //                                        var dictionaryCompetenceTranslation = dco.DictionaryCompetenceTranslations.SingleOrDefault(dclt => dclt.LanguageId == languageId);
                //                                        if (dictionaryCompetenceTranslation != null)
                //                                            dictionaryCompetenceModel.Name = dictionaryCompetenceTranslation.Text;

                //                                        dictionaryCompetenceModel.DictionaryLevels = dco.DictionaryLevels
                //                                            .OrderBy(dl => dl.Name)
                //                                            .Select(dl =>
                //                                                        {
                //                                                            var dictionaryLevelModel = Mapper.Map<DictionaryLevelModel>(dl);

                //                                                            var dictionaryLevelTranslation = dl.DictionaryLevelTranslations.SingleOrDefault(dclt => dclt.LanguageId == languageId);
                //                                                            if (dictionaryLevelTranslation != null)
                //                                                                dictionaryLevelModel.Name = dictionaryLevelTranslation.Text;

                //                                                            dictionaryLevelModel.DictionaryIndicators = dl.DictionaryIndicators
                //                                                                .OrderBy(di => di.Name)
                //                                                                .Select(di =>
                //                                                                            {
                //                                                                                var dictionaryIndicatorModel = Mapper.Map<DictionaryIndicatorModel>(di);

                //                                                                                var dictionaryIndicatorTranslation = di.DictionaryIndicatorTranslations.SingleOrDefault(dclt => dclt.LanguageId == languageId);
                //                                                                                if (dictionaryIndicatorTranslation != null)
                //                                                                                    dictionaryIndicatorModel.Name = dictionaryIndicatorTranslation.Text;

                //                                                                                return dictionaryIndicatorModel;
                //                                                                            }).ToList();
                //                                                            return dictionaryLevelModel;
                //                                                        }).ToList();
                //                                        return dictionaryCompetenceModel;
                //                                    }).ToList();
                //                    return dictionaryClusterModel;
                //                })
                //    .ToList();

                //return PartialView(dictionaryModel);

                //TODO: remove this

                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                return HandleError(exception, isPartial: true);
            }
        }
    }
}
