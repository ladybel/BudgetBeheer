﻿using System;
using System.Collections.Generic;
using System.Linq;
using SyntraAB.Tools.Extensions;
using System.IO;
using System.Text.Json;
using Syntra.Data.Models;

namespace Syntra.Data.Models
{
    public class BookingLijst
    {
        public const string DataFile = "Booking.dat";
        public string LastError { get; protected set; } = "";
        public List<Booking> Members { get; set; } = new List<Booking>();
        public int Count { get => Members?.Count > 0 ? Members.Count : 0; }


        public BookingLijst()
        {
           
        }

        public bool Import()
        {
            string data = GetType().GetEmbeddedResource("boekingen.json");

            if (data.NotEmpty())
            {
                try
                {

                    var jsonData = JsonSerializer.Deserialize<List<Booking>>(data);
                    if (data != null)
                    {

                        foreach (var bk in jsonData)
                        {
                            bk.DatumStr = bk.Datum.ToShortDateString();
                            if (Members.Count > 0)
                            {
                                var item = Members.Where(t => t.ID == bk.ID).FirstOrDefault();
                                if (item != null)
                                {
                                    item.Klant_ID = bk.Klant_ID > 0 ? bk.Klant_ID: item.Klant_ID;
                                    item.Tafel = bk.Tafel > 0 ?bk.Tafel : item.Tafel;
                                    item.Datum = bk.Datum != null ? bk.Datum : item.Datum;
                                    item.DatumStr = bk.DatumStr.Length > 0 ? bk.DatumStr : item.DatumStr;
                                }

                                else
                                {
                                    Members.Add(bk);

                                }


                            }

                            else Members.AddRange(jsonData);
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    LastError = ex.ToString();
                    return false;
                }
            }


            else return false;
        }
    }
}


