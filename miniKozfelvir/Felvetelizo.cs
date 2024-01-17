using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace miniKozfelvir
{
    public class Felvetelizo
    {
        public const string CSVFEJ = "om_azonosito;nev;email;szuletesi_datum;ertesitesi_cim;matek_eredmeny;magyar_eredmeny";

        string omAzon;
        string nev;
        string email;
        DateTime szuletesiDatum;
        string ertesitesiCim;
        byte? matek;
        byte? magyar;

        public Felvetelizo(string omAzon, string nev, string email, DateTime szuletesiDatum, string ertesitesiCim, byte? matek, byte? magyar)
        {
            this.omAzon = omAzon;
            this.nev = nev;
            this.email = email;
            this.szuletesiDatum = szuletesiDatum;
            this.ertesitesiCim = ertesitesiCim;
            this.matek = matek;
            this.magyar = magyar;
        }

        public Felvetelizo(string csvSor)
        {
            string[] mezok = csvSor.Split(';');

            this.omAzon = mezok[0];
            this.nev = mezok[1];
            this.email = mezok[2];
            this.szuletesiDatum = DateTime.Parse(mezok[3]);
            this.ertesitesiCim = mezok[4];

            byte _matek;
            this.matek = Byte.TryParse(mezok[5], out _matek) ? (byte?)_matek : null;

            byte _magyar;
            this.magyar = Byte.TryParse(mezok[6], out _magyar) ? (byte?)_magyar : null;
        }

        public string OmAzon { get => omAzon; }
        public string Nev { get => nev; }
        public string Email { get => email; }
        public DateTime SzuletesiDatum { get => szuletesiDatum; }
        public string ErtesitesiCim { get => ertesitesiCim; }
        public byte? Matek { get => matek; }
        public byte? Magyar { get => magyar; }

        public override string ToString()
        {
            return $"{omAzon};{nev};{email};{szuletesiDatum};{ertesitesiCim};{matek};{magyar}";
        }
    }
}