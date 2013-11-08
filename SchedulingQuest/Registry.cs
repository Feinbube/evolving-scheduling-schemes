using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulingQuest
{
    class Registry
    {
        public static Random Random = new Random(2000);

        public static bool Waiting = false;
        public static bool PrintSchedules = false;

        public static int Scale = 50;

        public static List<Scheduler> AllSchedulers()
        {
            List<Scheduler> schedulers = new List<Scheduler>();

            schedulers.AddRange(StateOfTheArt.Schedulers());
            schedulers.AddRange(Optimums.Schedulers());
            // schedulers.AddRange(SoD.Schedulers());
            // schedulers.AddRange(S7D.Schedulers()); perform badly :(

            return schedulers;
        }

        public static List<Challenge> QualityChallenges()
        {
            List<Challenge> challenges = new List<Challenge>();

            //challenges.Add(new SyntheticUtilization());
            //challenges.Add(new SlackDhall());
            challenges.Add(new Dhall());
            //challenges.Add(new Lemma3());
            //challenges.Add(new ToyExample());
            //challenges.Add(new WikiRMS());
            challenges.Add(new RMS());

            //challenges.Add(new LLURMS());

            //challenges.Add(new CompleteChallenges());

            return challenges;
        }

        public static List<Challenge> StressChallenges()
        {
            List<Challenge> challenges = new List<Challenge>();

            //challenges.Add(new EasyStress());

            //challenges.Add(new PeriodicChallenges());
            //challenges.Add(new CompleteChallenges());

            //challenges.Add(new WeirdChallenges());

            //challenges.Add(new Andersson_005());
            challenges.Add(new Andersson_010());
            //challenges.Add(new Andersson_020());
            //challenges.Add(new Andersson_050());
            //challenges.Add(new Andersson_100());
            //challenges.Add(new Andersson_200());
            //challenges.Add(new Andersson_500());

            return challenges;
        }

        public static List<int> ProcessorCounts()
        {
            return new List<int> { 1, 2, 4 }; //, 7, 8, 10, 12 }; //, 13, 16};//, 20, 24, 25, 32, 50, 64};
        }
    }
}
