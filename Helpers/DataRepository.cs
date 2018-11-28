using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class PatientRepository
    {
        private DataCiper dataCiper;

        private XElement root;
        private string path;

        public PatientRepository(string path, DataCiper dataCiper)
        {
            if (!File.Exists(path))
            {
                this.CreateDoc(path);
            }

            this.dataCiper = dataCiper;

            this.root = XElement.Load(path);
            this.path = path;
        }

        public Guid Create(Patient patient)
        {
            var id = Guid.NewGuid();
            var encryptedMail = this.dataCiper.Encrypt(patient.Email);

            var element = new XElement("patient");
                element.Add(new XElement("FirstName", patient.FirstName));
                element.Add(new XElement("LastName", patient.LastName));
                element.Add(new XElement("Phone", patient.Phone));
                element.Add(new XElement("Gender", patient.Gender));
                element.Add(new XElement("Notes", patient.Notes));
                element.Add(new XElement("CreatedDate", DateTime.Now));
                element.Add(new XElement("LasUpdatedDate", DateTime.Now));
                element.Add(new XAttribute("Id", id));
                element.Add(new XAttribute("IsDeleted", false));

            var emailElement = new XElement("Email");
            
            foreach (var b in encryptedMail)
            {
                emailElement.Add(new XElement("b", b));
            }

            element.Add(emailElement);

            this.root.Add(element);
            this.root.Save(this.path);

            return id;
        }

        public void Update(Patient patient, Guid id)
        {
            var element = this.FindById(id);

            if (element != null)
            {
                var encryptedMail = this.dataCiper.Encrypt(patient.Email);

                element.Element("FirstName").SetValue(patient.FirstName);
                element.Element("LastName").SetValue(patient.LastName);
                element.Element("Phone").SetValue(patient.Phone);
                element.Element("Gender").SetValue(patient.Gender);
                element.Element("Notes").SetValue(patient.Notes);
                element.Element("LasUpdatedDate").SetValue(DateTime.Now);

                element.Element("Email").Remove();

                var emailElement = new XElement("Email");

                foreach (var b in encryptedMail)
                {
                    emailElement.Add(new XElement("b", b));
                }

                element.Add(emailElement);

                this.root.Save(this.path);
            }
        }

        public void Delete(Guid id)
        {
            var element = this.FindById(id);

            if (element != null)
            {
                element.Element("LasUpdatedDate").SetValue(DateTime.Now);
                element.Attribute("IsDeleted").SetValue(true);

                this.root.Save(this.path);
            }
        }

        public Patient Patient(Guid id)
        {
            var result = default(Patient);
            var element = this.FindById(id);

            if (element != null)
            {
                result = this.BuildModel(element);
            }

            return result;
        }

        public List<Patient> List()
        {
            return this.root.Descendants("patient")
                .Where(element => !bool.Parse(element.Attribute("IsDeleted").Value))
                .Select(element => this.BuildModel(element)).ToList();
        }

        private Patient BuildModel(XElement element)
        {
            var emailByteElements = element.Element("Email").Descendants("b").ToList();
            var bytes = new byte[emailByteElements.Count()];

            for (var index = 0; index < emailByteElements.Count(); index++)
            {
                bytes[index] = byte.Parse(emailByteElements[index].Value);
            }

            return new Patient
            {
                Id = Guid.Parse(element.Attribute("Id").Value),
                FirstName = element.Element("FirstName").Value,
                LastName = element.Element("LastName").Value,
                Phone = element.Element("Phone").Value,
                Email = this.dataCiper.Decrypt(bytes),
                Gender = element.Element("Gender").Value,
                Notes = element.Element("Notes").Value,
                CreatedDate = DateTime.Parse(element.Element("CreatedDate").Value),
                LasUpdatedDate = DateTime.Parse(element.Element("LasUpdatedDate").Value),
                IsDeleted = bool.Parse(element.Attribute("IsDeleted").Value)
            };
        }

        private XElement FindById(Guid id)
        {
            var element = this.root.Descendants("patient")
                .FirstOrDefault(e => Guid.Parse(e.Attribute("Id").Value) == id);

            return element;
        }

        private void CreateDoc(string path)
        {
            (new XDocument(new XElement("patients"))).Save(path);
        }
    }
}