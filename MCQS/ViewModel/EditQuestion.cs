using MCQS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCQS.ViewModel
{
    public class EditQuestion
    {
        public IEnumerable<Question> questions { get; set; }
        public Question question { get; set; }

    }
}