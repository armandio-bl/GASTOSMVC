using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GASTOSMVC.Models
{
    public class Combos
    {
        public List<SelectListItem> FNLLenaCombo(string WText, string WValue, string WSP, string WPar, params string[] oVal)
        {
            DataLayer DL = new DataLayer();
            List<SelectListItem> items = new List<SelectListItem>();
            var rcbx = DL.SPRdrSPX(WSP, 0, WPar, oVal);
            while (rcbx.Read())
            {
                items.Add(new SelectListItem
                {
                    Text = rcbx[WText].ToString(), Value = rcbx[WValue].ToString()
                });
            }
            rcbx.Close(); rcbx.Dispose(); rcbx = null;  DL.Dispose(); DL = null;
            return items;
        }

        public List<SelectListItem> FNLLenaCB(string WValor, string  WTextA)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string[] oValue = WValor.Split('|'); string[] oText = WTextA.Split('|');
            for (int K = 0; K <= oValue.Length - 1; K++) {
                items.Add(new SelectListItem
                {
                    Text = oText[K].ToString(), Value = oValue[K].ToString()
                });
            }
            Array.Clear(oValue, 0, oValue.Length); oValue = null; Array.Clear(oText, 0, oText.Length); oText = null;
            return items;
        }
    }
}