using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Infastructure.Models
{
	public enum ratings { one, two, three, four, five}

	public class Feedback
	{
		public int FeedbackId { get; set; }
		public ratings Rating { get; set; }

		public string UserFeedBack { get; set; }

		public int HamperId { get; set; }

		public Guid ApplicationUserId { get; set; }

	}
}
