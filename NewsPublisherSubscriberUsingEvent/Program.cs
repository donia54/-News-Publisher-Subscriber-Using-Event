using System;
using System.Security.Cryptography;

namespace NewsPublisherSubscriberUsingEvent
{

    public delegate void NewsPublishedEventHandler(object sender, NewsEventArgs e);//1-Define a Delegate

    public class NewsEventArgs : EventArgs // 2-Event Arg Class
    {
        public string title { get; }
        public string content { get;  }

        public NewsEventArgs(string title,string content)
        {
            this.title = title;
            this.content = content;
        }
    }

    public class NewsPublisher // Define the Event and Raise It
    {
        public event NewsPublishedEventHandler NewsPublished;

        public void PublishNews(string title,string content)
        {
            OnNewsPublished(new NewsEventArgs(title, content));
        }

        //using virtual to give any subclass of NewsPublisher the ability to override and customize the behavior when the event is triggered.
        protected virtual void OnNewsPublished(NewsEventArgs e)
        {
            NewsPublished?.Invoke(this, e);//Raise
        }
    }

    //Define Subscriber Classes
    public class EmailSubscriber 
    {
        public void OnNewsPublished(object sender, NewsEventArgs e)
        {
            Console.WriteLine($"Email: New Article Published!\nTitle: {e.title}\nContent: {e.content}\n");
        }
    }
    public class SMSSubscriber
    {
        public void OnNewsPublished(object sender, NewsEventArgs e)
        {
            Console.WriteLine($"SMS: New Article Published!\nTitle: {e.title}\n");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            NewsPublisher publisher = new NewsPublisher();

            EmailSubscriber emailSubscriber = new EmailSubscriber();
            SMSSubscriber smsSubscriber = new SMSSubscriber();

            publisher.NewsPublished += emailSubscriber.OnNewsPublished;
            publisher.NewsPublished += smsSubscriber.OnNewsPublished;

            publisher.PublishNews("Holllla", "Today,blah blah blah");
            publisher.PublishNews("OLLLL", "SOS");


            Console.ReadKey();
        }
    }
}
