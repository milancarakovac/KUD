using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface IzvodjackaGrupaKoreografijaDao
    {
        public IzvodjackaGrupaKoreografija GetById(int idKoreografija, int idIzvodjackaGrup);
        public int Insert(IzvodjackaGrupa grupa, Koreografija koreografija);
        public int Delete(int idKoreografija, int idIzvodjackaGrup);
    }
}
