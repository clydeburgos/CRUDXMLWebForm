using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class PatientForm : System.Web.UI.Page
    {
        private PatientRepository repository = new PatientRepository(HttpContext.Current.Server.MapPath("patients.xml"), new DataCiper("somerandomkey"));

        private Guid patientId;
        private bool isUpdate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                this.patientId = Guid.Parse(Request.QueryString["Id"].ToString());
                this.isUpdate = true;

                if (!IsPostBack)
                {
                    var patient = this.repository.Patient(this.patientId);

                    txtFirstName.Value = patient.FirstName;
                    txtLastName.Value = patient.LastName;
                    txtPhone.Value = patient.Phone;
                    txtEmail.Value = patient.Email;
                    slcGender.Value = patient.Gender;
                    txtNotes.Value = patient.Notes;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var patient = new Patient
            {
                FirstName = txtFirstName.Value,
                LastName = txtLastName.Value,
                Phone = txtPhone.Value,
                Email = txtEmail.Value,
                Gender = slcGender.Value,
                Notes = txtNotes.Value
            };

            if (this.isUpdate)
            {
                this.repository.Update(patient, this.patientId);
            }
            else
            {
                this.repository.Create(patient);
            }
            

            Response.Redirect("~/");
        }
    }
}