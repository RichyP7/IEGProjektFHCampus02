using CommonServiceLib.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyCreatorService.DomainLogic.SurveyAggregate
{
    public class Survey :Entity
    {
        public Survey(int id)
        {
            this.Id = id;
        }
        public List<Question> Questions { get; set; }
    }
}
