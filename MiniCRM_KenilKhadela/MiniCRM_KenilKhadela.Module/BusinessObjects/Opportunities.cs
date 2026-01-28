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
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraGauges.Core.Model;

namespace MiniCRM_KenilKhadela.Module.BusinessObjects;

public enum TimeFrame
{
    Unknown=0,
    ThisYear=1,
    ThisQuater=2,
    NextQuater=3,
    Immediate=4
}

public enum BudgetStatus
{
    NoCommitedBudget=0,
    MayBuy=1,
    WillBuy=2,
    CanBuy=3
}

public enum ForecastCategory
{
    Pipeline=0,
    BestCase=1,
    Commited=2,
    Ommited=3,
    Won=4,
    Lost=5
}

public enum PurchaseProcess
{
    Unknown=0,
    Officer=1,
    Individual=2
}

[DefaultProperty(nameof(Topic))]
public class Opportunities : BaseObject {
    // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://docs.devexpress.com/eXpressAppFramework/113146/business-model-design-orm/business-model-design-with-xpo/base-persistent-classes).
    // Use CodeRush to create XPO classes and properties with a few keystrokes.
    // https://docs.devexpress.com/CodeRushForRoslyn/118557
    public Opportunities(Session session)
        : base(session) {
    }

    public override void AfterConstruction() {
        base.AfterConstruction();
    }

    private string topic;
    private TimeFrame timeFrame;
    private TransactionCurrency transactionCurrency;
    private decimal budget;
    private BudgetStatus budgetStatus;
    private ForecastCategory forecastCategory;

    private DateTime estimatedCloseDate;
    private DateTime actualCloseDate;

    private string description;

    [Size(SizeAttribute.Unlimited)]
    public string Topic
    {
        get=> topic;
        set => SetPropertyValue(nameof(Topic),ref topic, value);
    }

    public TimeFrame TimeFrame
    {
        get => timeFrame;
        set=> SetPropertyValue(nameof(TimeFrame),ref timeFrame, value);
    }

    public TransactionCurrency TransactionCurrency
    {
        get => transactionCurrency;
        set=> SetPropertyValue(nameof(TransactionCurrency),ref transactionCurrency, value);
    }

    public decimal Budget
    {
        get=> budget;
        set=> SetPropertyValue(nameof(Budget),ref budget, value);
    }

    public BudgetStatus BudgetStatus
    {
        get => budgetStatus;
        set=> SetPropertyValue(nameof(BudgetStatus),ref budgetStatus, value);
    }

    public ForecastCategory ForecastCategory
    {
        get => forecastCategory;
        set=> SetPropertyValue(nameof(ForecastCategory),ref forecastCategory, value);
    }

    [Size(SizeAttribute.Unlimited)]
    public string Description
    {
        get=> description;
        set=> SetPropertyValue(nameof(Description),ref description, value);
    }

    public DateTime EstimatedCloseDate
    {
        get=> estimatedCloseDate;
        set=> SetPropertyValue(nameof(EstimatedCloseDate),ref estimatedCloseDate, value);
    }

    public DateTime ActualCloseDate
    {
        get => actualCloseDate;
        set => SetPropertyValue(nameof(ActualCloseDate),ref actualCloseDate, value);
    }

    private string currentSituation;
    [Size(SizeAttribute.Unlimited)]
    public string CurrentSituation
    {
        get=> currentSituation;
        set=> SetPropertyValue(nameof(CurrentSituation),ref currentSituation, value);
    }

    private string customerNeed;
    [Size(SizeAttribute.Unlimited)]
    public String CustomerNeed
    {
        get => customerNeed;
        set=> SetPropertyValue(nameof(CustomerNeed),ref customerNeed, value);
    }

    [RuleFromBoolProperty("ActualCloseDateValidation", DefaultContexts.Save, CustomMessageTemplate = "Actual close date can not be earlier than Estimated close date")]
    public bool IsActualCloseDateValid
    {
        get { return ActualCloseDate>=EstimatedCloseDate; }
    }

    private string solution;
    [Size(SizeAttribute.Unlimited)]
    [XafDisplayName("PROPOSED SOLUTION")]
    public String Solution
    {
        get=> solution;
        set=> SetPropertyValue(nameof(Solution),ref solution, value);
    }

    private Contact contact;
    [Association("Contact-Opportunities")]
    [VisibleInDetailView(false)]
    public Contact Contact
    {
        get => contact;
        set => SetPropertyValue(nameof(Contact), ref contact, value);
    }

    private Accounts accounts;
    [Association("Opportunities-Accounts")]
    [VisibleInDetailView(false)]
    public Accounts Accounts
    {
        get=> accounts;
        set=> SetPropertyValue(nameof(Accounts),ref accounts, value);
    }
}