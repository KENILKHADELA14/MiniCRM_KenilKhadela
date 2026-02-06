using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace MiniCRM_KenilKhadela.Module.BusinessObjects;

[DefaultClassOptions]
public class TransactionCurrency : BaseObject {
    public TransactionCurrency(Session session)
        : base(session) {
    }
    public override void AfterConstruction() {
        base.AfterConstruction();
    }

    private string isoCode;
    private string name;
    private string symbol;
    private int precision;
    private decimal exchangeRate;

    public string IsoCode
    {
        get => isoCode;
        set=> SetPropertyValue(nameof(IsoCode),ref isoCode,value);
    }

    public string Name
    {
        get => name;
        set => SetPropertyValue(nameof(Name),ref name,value);
    }

    public string Symbol
    {
        get=> symbol;
        set => SetPropertyValue(nameof(Symbol),ref symbol,value);
    }

    public decimal ExchangeRate
    {
        get => exchangeRate;
        set => SetPropertyValue(nameof(ExchangeRate),ref exchangeRate,value);
    }

    public int Precision
    {
        get=> precision;
        set => SetPropertyValue(nameof(Precision),ref precision,value);
    }
    
}