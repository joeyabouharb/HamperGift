using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Infastructure.Models;
namespace Project_Infastructure.services
{
	public class DataMap
	{
		public List<MapCartData> mapCartDatas(IQueryable<Hamper> hampers, IQueryable<Invoice> invoices)
		{
			MapCartData map = new MapCartData();
			IList<MapCartData> cartDatas = null;
			foreach (var hamper in hampers)
			{
				foreach (var invoice in invoices)
				{


					if (hamper.HamperId == invoice.HamperId)
					{
						map = new MapCartData
						{
							InvoiceId = invoice.InvoiceId,
							HamperName = hamper.HamperName,
							Cost = (hamper.Cost * invoice.Quantity),
							Quantity = invoice.Quantity
						};

					}
					if (cartDatas.Contains(map))
					{
						continue;
					}
					cartDatas.Add(map);


				}
			}
			return cartDatas.ToList();
		}

	}
}
