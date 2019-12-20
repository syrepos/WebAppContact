namespace ContactWebApp.Migrations
{
    using ContactWebApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactWebApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ContactWebApp.Models.ApplicationDbContext context)
        {
            CreateStates(context);
        }
        private void CreateStates(ApplicationDbContext context)
        {
            foreach(var state in SeedStates)
            {
                var exist = context.States.FirstOrDefault(x => x.Name.Equals(state.Name, StringComparison.OrdinalIgnoreCase)
                                        && x.Abbreviation.Equals(state.Abbreviation, StringComparison.OrdinalIgnoreCase));
                if (exist == null)
                {
                    context.States.Add(state);

                }
            }
        }
        private static List<State> SeedStates = new List<State>
        {
            new State() { Name="Nabeul", Abbreviation="NB"},
            new State() { Name="Tunis", Abbreviation="TN"},
            new State() { Name="Bizerte", Abbreviation="BZ"},
            new State() { Name="Sousse", Abbreviation="SO"},
            new State() { Name="Tabarka", Abbreviation="TK"},
        };
    }
}
