using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Infastructure.Models
{
	public class Feedback
	{
		public int FeedbackId { get; set; }
		public ratings Rating { get; set; }

		public string UserFeedBack { get; set; }

		public int HamperId { get; set; }
	}
}
