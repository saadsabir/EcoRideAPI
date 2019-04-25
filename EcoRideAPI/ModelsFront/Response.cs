using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoppChoppWS.ModelsFront
{
    /// <summary>
    /// Type de reponse de tous les EndPoints
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
        /// <summary>
        /// numero de reponse
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// type de reponse
        /// </summary>
        public string commentaire { get; set; }
        public T resultat { get; set; }
        public string hash { get; set; }
    }

    public class NewCommandeResponse<T>
    {
        /// <summary>
        /// numero de reponse
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// type de reponse
        /// </summary>
        public string commentaire { get; set; }
        public T resultat { get; set; }
        public long idcommande { get; set; }
        public string voucher { get; set; }
        public string ostr { get; set; }
    }
}