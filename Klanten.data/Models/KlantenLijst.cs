﻿using System;
using System.Collections.Generic;
using System.Linq;
using SyntraAB.Tools.Extensions;
using System.IO;
using System.Text.Json;
using Syntra.Data.Models;

namespace Syntra.Data.Models
{
    public class KlantenLijst
    {
        public const string DataFile = "Klant.dat";
        public string LastError { get; protected set; } = "";
        public List<Klant> Members { get; set; } = new List<Klant>();
        public int Count { get => Members?.Count > 0 ? Members.Count : 0; }


        public KlantenLijst()
        {
           
        }

        public bool Import()
        {
            string data = GetType().GetEmbeddedResource("klanten.json");

            if (data.NotEmpty())
            {
                try
                {

                    var jsonData = JsonSerializer.Deserialize<List<Klant>>(data);
                    if (data != null)
                    {

                        foreach (var kl in jsonData)
                        {
                            kl.GebDatumStr = kl.GebDatum.ToShortDateString();
                            if (Members.Count > 0)
                            {
                                var item = Members.Where(t => t.ID == kl.ID).FirstOrDefault();
                                if (item != null)
                                {
                                    item.Naam = kl.Naam?.Length > 0 ? kl.Naam : item.Naam;
                                    item.Adres = kl.Adres?.Length > 0 ? kl.Adres : item.Adres;
                                    item.GebDatum = kl.GebDatum != null ? kl.GebDatum : item.GebDatum;
                                    item.GebDatumStr = kl.GebDatumStr.Length > 0 ? kl.GebDatumStr : item.GebDatumStr;
                                }

                                else
                                {
                                    Members.Add(kl);

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

