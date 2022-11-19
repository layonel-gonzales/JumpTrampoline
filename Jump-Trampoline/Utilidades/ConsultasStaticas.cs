using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jump_Trampoline.Utilidades
{
  public static class ConsultasStaticas
  {
    [Serializable]
    public class Dia {
      public string Id { get; set; }
      public string NombreDia { get; set; }
    }
    
    public static List<Dia> TraerDias() {
      List<Dia> list = new List<Dia>();
      list.Add(new Dia { Id = "1", NombreDia = "Lunes" });
      list.Add(new Dia { Id = "2", NombreDia = "Martes" });
      list.Add(new Dia { Id = "3", NombreDia = "Miercoles" });
      list.Add(new Dia { Id = "4", NombreDia = "Jueves" });
      list.Add(new Dia { Id = "5", NombreDia = "Viernes" });
      list.Add(new Dia { Id = "6", NombreDia = "Sabado" });
      list.Add(new Dia { Id = "7", NombreDia = "Domingo" });
      return list;
    }
  }
}