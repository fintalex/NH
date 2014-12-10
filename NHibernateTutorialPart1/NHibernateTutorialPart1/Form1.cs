using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace NHibernateTutorialPart1
{
	public partial class Form1 : Form
	{
		private Configuration myConfiguration;
		private ISessionFactory mySessionFactory;
		private ISession mySession;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			myConfiguration = new Configuration();

			myConfiguration.AddAssembly("NHibernateTutorialPart1");
			myConfiguration.Configure();
			mySessionFactory = myConfiguration.BuildSessionFactory();
			mySession = mySessionFactory.OpenSession();


			// Adding
			using (mySession.BeginTransaction())
			{
				Contact loContact = new Contact { FirstName = "nik", LastName = "Harchenko" };
				mySession.Save(loContact);

				mySession.Transaction.Commit();

			}

			// List contact
			using (mySession.BeginTransaction())
			{
				ICriteria criteria = mySession.CreateCriteria<Contact>();
				IList<Contact> list = criteria.List<Contact>();

				mySession.Transaction.Commit();
			}
		}
	}
}
