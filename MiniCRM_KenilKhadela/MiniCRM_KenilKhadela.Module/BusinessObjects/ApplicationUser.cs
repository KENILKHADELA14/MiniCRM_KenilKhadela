using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using System.ComponentModel;

namespace MiniCRM_KenilKhadela.Module.BusinessObjects;
[MapInheritance(MapInheritanceType.ParentTable)]
[DefaultProperty(nameof(UserName))]
[CurrentUserDisplayImage(nameof(Photo))]
[NavigationItem(false)]
public class ApplicationUser : PermissionPolicyUser, IAuthenticationStandardUser
{
    public ApplicationUser(Session session) : base(session) { }

    private string firstName;
    public string FirstName
    {
        get => firstName;
        set => SetPropertyValue(nameof(FirstName), ref firstName, value);
    }

    private string lastName;
    public string LastName
    {
        get => lastName;
        set => SetPropertyValue(nameof(LastName), ref lastName, value);
    }

    private string department;
    [VisibleInListView(false)]
    public string Department
    {
        get => department;
        set => SetPropertyValue(nameof(Department), ref department, value);
    }

    private MediaDataObject photo;
    [ImageEditor]
    [VisibleInListView(false)]
    public MediaDataObject Photo
    {
        get => photo;
        set => SetPropertyValue(nameof(Photo), ref photo, value);
    }

    private bool changePasswordOnFirstLogon;
    [VisibleInListView(false)]
    public bool ChangePasswordOnFirstLogon
    {
        get => changePasswordOnFirstLogon;
        set => SetPropertyValue(nameof(ChangePasswordOnFirstLogon), ref changePasswordOnFirstLogon, value);
    }

    bool IAuthenticationStandardUser.ComparePassword(string password)
    {
        return PasswordCryptographer.VerifyHashedPasswordDelegate(StoredPassword, password);
    }
}
