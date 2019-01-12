using System;
using System.Collections.Generic;
using System.Text;

namespace CommonServiceLib.Dto
{
    public class QuestionDto
    {
        public string QuestionText { get; set; }
        public List<AnswerDto> Answers{ get; set; }
    }
}
