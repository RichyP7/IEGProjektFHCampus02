﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonServiceLib.Dto
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public List<QuestionDto> SurveyQuestions { get; set; }
    }
}
