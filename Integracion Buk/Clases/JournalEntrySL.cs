using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integracion_Buk.Clases
{
    public class JournalEntrySL
    {
        // Root myDeserializedClass = JsonConvert.Deserializestring<Root>(myJsonResponse);
        public class JournalEntryLine
        {
            public int Line_ID { get; set; }
            public string AccountCode { get; set; }
            public double Debit { get; set; }
            public double Credit { get; set; }
            public double FCDebit { get; set; }
            public double FCCredit { get; set; }
            public string FCCurrency { get; set; }
            public string DueDate { get; set; }
            public string ShortName { get; set; }
            public string ContraAccount { get; set; }
            public string LineMemo { get; set; }
            public string ReferenceDate1 { get; set; }
            public string ReferenceDate2 { get; set; }
            public string Reference1 { get; set; }
            public string Reference2 { get; set; }
            public string ProjectCode { get; set; }
            public string CostingCode { get; set; }
            public string TaxDate { get; set; }
            public string BaseSum { get; set; }
            public string TaxGroup { get; set; }
            public double DebitSys { get; set; }
            public double CreditSys { get; set; }
            public string VatDate { get; set; }
            public string VatLine { get; set; }
            public string SystemBaseAmount { get; set; }
            public string VatAmount { get; set; }
            public string SystemVatAmount { get; set; }
            public string GrossValue { get; set; }
            public string AdditionalReference { get; set; }
            public string CheckAbs { get; set; }
            public string CostingCode2 { get; set; }
            public string CostingCode3 { get; set; }
            public string CostingCode4 { get; set; }
            public string TaxCode { get; set; }
            public string TaxPostAccount { get; set; }
            public string CostingCode5 { get; set; }
            public string LocationCode { get; set; }
            public string ControlAccount { get; set; }
            public string EqualizationTaxAmount { get; set; }
            public string SystemEqualizationTaxAmount { get; set; }
            public string TotalTax { get; set; }
            public string SystemTotalTax { get; set; }
            public string WTLiable { get; set; }
            public string WTRow { get; set; }
            public string PaymentBlock { get; set; }
            public string BlockReason { get; set; }
            public string FederalTaxID { get; set; }
            //public string BPLID { get; set; }
            //public string BPLName { get; set; }
            public string VATRegNum { get; set; }
            public string PaymentOrdered { get; set; }
            public string ExposedTransNumber { get; set; }
            public int DocumentArray { get; set; }
            public int DocumentLine { get; set; }
            //public string CostElementCode { get; set; }
            //public string Cig { get; set; }
            //public string Cup { get; set; }
            //public string IncomeClassificationCategory { get; set; }
            //public string IncomeClassificationType { get; set; }
            //public string ExpensesClassificationCategory { get; set; }
            //public string ExpensesClassificationType { get; set; }
            //public string VATClassificationCategory { get; set; }
            //public string VATClassificationType { get; set; }
            //public string VATExemptionCause { get; set; }
            public string U_EXX_CTAPAT { get; set; }
            public string U_EXX_SUJPER { get; set; }
            public string U_EXX_INCPER { get; set; }
            public string U_EXX_PORPER { get; set; }
            public string U_EXX_MONPER { get; set; }
            public string U_EXX_MONAFEPER { get; set; }
            public string U_EXX_TIPCON { get; set; }
            public string U_EXX_BPChCode { get; set; }
            public string U_EXO_TDOFAC { get; set; }
            public string U_EXO_FACNUM { get; set; }
            public string U_EXO_FACRAZ { get; set; }
            public string U_EXO_PRFNUM { get; set; }
            public string U_EXO_FACNET { get; set; }
            public string U_EXO_FACDAT { get; set; }
            public string U_EXO_FACVEN { get; set; }
            public string U_EXO_ESFCOD { get; set; }
            public string U_EXO_DES_GRUPO { get; set; }
            public string U_EXO_TIPO3 { get; set; }
            public string U_EXO_DES_SUBGRUPO { get; set; }
            public string U_EXO_TARCOD { get; set; }
            public string U_EXO_TARDES { get; set; }
            public string U_EXO_TOTPAR { get; set; }
            public string U_EXO_MEDOBS { get; set; }
            public string U_EXO_MEDCOD { get; set; }
            public string U_EXO_MEDNAM { get; set; }
            public string U_EXO_HONTIP { get; set; }
            public string U_ABoeKey { get; set; }
            public string U_ADocEntry { get; set; }
            public string U_EXO_CardCode { get; set; }
            public string U_EXO_FACSER { get; set; }
            public string U_EXX_POSFLUJO { get; set; }
            public string U_EXX_DETRACCION { get; set; }
            public string U_EXO_PACNAM { get; set; }
            //public List<string> CashFlowAssignments { get; set; }
        }

     
        
            public string ReferenceDate { get; set; }
            public string Memo { get; set; }
            public string Reference { get; set; }
            public string Reference2 { get; set; }
            public string TransactionCode { get; set; }
            public string ProjectCode { get; set; }
            public string TaxDate { get; set; }
            public int JdtNum { get; set; }
            public string Indicator { get; set; }
            public string UseAutoStorno { get; set; }
            public string StornoDate { get; set; }
            public string VatDate { get; set; }
            public int Series { get; set; }
            public string StampTax { get; set; }
            public string DueDate { get; set; }
            public string AutoVAT { get; set; }
            public int Number { get; set; }
            public int FolioNumber { get; set; }
            public string FolioPrefixString { get; set; }
            public string ReportEU { get; set; }
            public string Report347 { get; set; }
            public string Printed { get; set; }
            public string LocationCode { get; set; }
            public string OriginalJournal { get; set; }
            public int Original { get; set; }
            public string BaseReference { get; set; }
            public string BlockDunningLetter { get; set; }
            public string AutomaticWT { get; set; }
            public string WTSum { get; set; }
            public string WTSumSC { get; set; }
            public string WTSumFC { get; set; }
            public string SignatureInputMessage { get; set; }
            public string SignatureDigest { get; set; }
            public string CertificationNumber { get; set; }
            public string PrivateKeyVersion { get; set; }
            public string Corisptivi { get; set; }
            public string Reference3 { get; set; }
            public string DocumentType { get; set; }
            public string DeferredTax { get; set; }
            public string BlanketAgreementNumber { get; set; }
            public string OperationCode { get; set; }
            public string ResidenceNumberType { get; set; }
            public string ECDPostingType { get; set; }
            public string ExposedTransNumber { get; set; }
            public string PointOfIssueCode { get; set; }
            public string Letter { get; set; }
            public string FolioNumberFrom { get; set; }
            public string FolioNumberTo { get; set; }
            public string IsCostCenterTransfer { get; set; }
            public string ReportingSectionControlStatementVAT { get; set; }
            public string ExcludeFromTaxReportControlStatementVAT { get; set; }
            public string SAPPassport { get; set; }
            public string Cig { get; set; }
            public string Cup { get; set; }
            public string AdjustTransaction { get; set; }
            public string AttachmentEntry { get; set; }
            public string U_EXX_TIPCON { get; set; }
            public string U_EXX_APER20 { get; set; }
            public string U_EXO_TIPO { get; set; }
            public string U_EXO_SEDE { get; set; }
            public string U_EXX_DETLOT { get; set; }
            public string U_EXX_AUTDET { get; set; }
            public string U_EXX_PROORI { get; set; }
            public string U_CP_VARESC { get; set; }
            public string U_EXX_PRIPAG { get; set; }
            public List<JournalEntryLine> JournalEntryLines { get; set; }
            //public List<string> WithholdingTaxDataCollection { get; set; }
            //public List<string> ElectronicProtocols { get; set; }
        


    }
}
