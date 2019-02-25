using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFiscal.Models.Afip
{
    public class NF_ENVIO
    {
        public int _ID_ENVIO;

        public System.Nullable<int> _ID_VENDA;

        public System.Nullable<int> _ID_CLIENTE;

        public System.Nullable<decimal> _VALOR_EMITIDO;

        public System.Nullable<System.DateTime> _DT_ENVIO;

        public string _NOME;

        public string _EMAIL;

        public string _CIDADE;

        public string _ENDERECO;

        public string _NUMERO;

        public string _DESCRICAO;

        public string _CODIGO_SERVICO;

        public System.Nullable<System.DateTime> _DT_RETORNO;

        public string _NUMERO_NF;

        public bool _FL_ENVIO;

        public bool _FL_ERRO;

        public string _MENSAGEM_RETORNO;

        public string _CODIGO_CIDADE;

        public string _CEP;

        public System.Nullable<int> _ID_FILIAL;

        public string _BAIRRO;

        public string _ESTADO;

        public string _STATUS_NF;

        public bool _FL_AGUARDANDO_ENVIO;

        public System.Nullable<int> _ID_RECEBIMENTO;

        public System.Nullable<System.DateTime> _DT_EMISSAO;

        public string _JSON_RETORNO;

        public string _FORNECEDOR_NF;

        public string _LINK_PDF;

        public string _LINK_XML;

        public System.Nullable<System.DateTime> _DATA_COMPETENCIA;

        public System.Nullable<int> _ID_FUNCIONARIO_ENVIO;

        public System.Nullable<int> _ID_TMP;

        public string _SOBRENOME;

        public string _ID_EXTERNO;

        public System.Nullable<System.DateTime> _DT_REENVIO;

        public string _NUMERO_NF_EMISSAO;

        public string _OBSERVACAO_NF;

        public string _TIPO_NF;

        public string _JSON_ENVIO;

        public System.Nullable<int> _ID_LOTE;

        public System.Nullable<int> _AR_TIPO_IVA;

        public System.Nullable<decimal> _AR_PERC_IVA;

        public System.Nullable<decimal> _AR_VALOR_IVA;

        public System.Nullable<int> _AR_TIPO_COMPROVANTE;

        public System.Nullable<int> _AR_ID_PONTO_VENDA;

        public System.Nullable<int> _AR_TIPO_DOCUMENTO;

        public string _AR_NUMERO_DOCUMENTO;

        public string _AR_RESULTADO;

        public string _AR_CAE;

        public string _AR_NUMERO_COMPROVANTE_RETORNO;

        public System.Nullable<System.DateTime> _AR_DT_VENCIMENTO_RETORNO;

        public object _FUNCIONARIO;

        public NF_LOTE _NF_LOTES;
    }

    public class NF_LOTE
    {
        private int ID_LOTE { get; set; }
        private DateTime? DT_CRIACAO { get; set; }
        private int? ID_FUNCIONARIO_CRIACAO { get; set; }
        private int? DATA_ENVIO { get; set; }
    }
}
