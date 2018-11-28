using System;
using System.Web;
using System.Web.UI.WebControls;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class _default : System.Web.UI.Page
    {
        private PatientRepository repository = new PatientRepository(HttpContext.Current.Server.MapPath("patients.xml"), new DataCiper(WebApplication1.Properties.Settings.Default.EncryptionKey));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.FillGrid();
            }
        }

        protected void PatientList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var patient = (Patient)e.Item.DataItem;
            var deleteBtn = (Button)e.Item.FindControl("btnDelete");
                deleteBtn.CommandArgument = patient.Id.ToString();
        }

        protected void PatientList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var patientId = Guid.Parse(e.CommandArgument.ToString());

            this.repository.Delete(patientId);
            this.FillGrid();
        }

        private void FillGrid()
        {
            PatientList.DataSource = repository.List();
            PatientList.DataBind();
        }
    }
}