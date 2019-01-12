using CommonServiceLib.Dto;
using SurveyCreatorService.DomainLogic.SurveyAggregate;

namespace SurveyCreatorService.Application
{
    public interface ISurveyService
    {
        Survey CreateSurvey();
    }
}