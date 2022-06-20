using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Jump_Trampoline.Utilidades {
  public static class Encrypt {

    public static string GetSHA256(string dato) {
      SHA256 sha256 = SHA256Managed.Create();
      ASCIIEncoding encoding = new ASCIIEncoding();
      byte[] stream = null;
      StringBuilder sb = new StringBuilder();
      stream = sha256.ComputeHash(encoding.GetBytes(dato));
      for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
      return sb.ToString();
    }
  }
}