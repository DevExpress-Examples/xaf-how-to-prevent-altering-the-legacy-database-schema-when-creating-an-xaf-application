using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;

namespace AspNetCore.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        string[] notes = {
            "works with customers until their problems are resolved and often goes an extra step to help upset customers be completely surprised by how far we will go to satisfy customers",
            "is very good at making team members feel included. The inclusion has improved the team's productivity dramatically",
            "is very good at sharing knowledge and information during a problem to increase the chance it will be resolved quickly",
            "actively elicits feedback from customers and works to resolve their problems",
            "creates an inclusive work environment where everyone feels they are a part of the team",
            "consistently keeps up on new trends in the industry and applies these new practices to every day work",
            "is clearly not a short term thinker - the ability to set short and long term business goals is a great asset to the company",
            "seems to want to achieve all of the goals in the last few weeks before annual performance review time, but does not consistently work towards the goals throughout the year",
            "does not yet delegate effectively and has a tendency to be overloaded with tasks which should be handed off to subordinates",
            "to be discussed with the top management..."
        };
        foreach(string note in notes) {
            Note noteObject = ObjectSpace.FirstOrDefault<Note>(n => n.Text.Contains(note));
            if(noteObject == null) {
                noteObject = ObjectSpace.CreateObject<Note>();
                noteObject.Text = note;
                noteObject.Author = "Somebody";
                noteObject.DateTime = DateTime.Now - TimeSpan.FromDays(1);
            }
        }
        ObjectSpace.CommitChanges();
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
}
