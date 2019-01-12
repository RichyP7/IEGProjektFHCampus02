using System;
using System.Collections.Generic;
using System.Text;

namespace CommonServiceLib.Dto
{
    public class QuestionDto
    {
        public QuestionDto(int questionId, string questionText)
        {
            QuestionId = questionId;
            QuestionText = questionText;
        }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerDto> Answers{ get; set; }
    }
}
