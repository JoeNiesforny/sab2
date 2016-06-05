using System.Collections.Generic;
using Machine.Specifications;
using ClusterManager;

namespace ClusterManagerTests
{
    [Tags("SettingCluserWithOneWorkers_ResultOneWorkerInReservedState")]
    [Subject("SettingCluserWithOneWorkers_ResultOneWorkerInReservedState")]
    public class SettingCluserWithOneWorkers_ResultOneWorkerInReservedState
    {
        static int result;
        static Message message;
        static Master master;
        static Worker worker1;

        Establish context = () =>
        {
            message = new Message();
            master = new Master(message);
            worker1 = new Worker(1, message);
        };

        Because of = () =>
        {
            result = master.AddWorkers(new List<Worker>() { worker1 });
        };

        It shouldReturnOneWorkerInReservedState = () =>
        {
            result.ShouldEqual(1);
        };
    }

    [Tags("SettingCluserWithTwoWorkers_ResultTwoWorkerInReservedState")]
    [Subject("SettingCluserWithTwoWorkers_ResultTwoWorkerInReservedState")]
    public class SettingCluserWithTwoWorkers_ResultTwoWorkerInReservedState
    {
        static int result;
        static Message message;
        static Master master;
        static Worker worker1;
        static Worker worker2;

        Establish context = () =>
        {
            message = new Message();
            master = new Master(message);
            worker1 = new Worker(1, message);
            worker2 = new Worker(2, message);
        };

        Because of = () =>
        {
            result = master.AddWorkers(new List<Worker>() { worker1, worker2 });
        };

        It shouldReturnOneWorkerInReservedState = () =>
        {
            result.ShouldEqual(2);
        };
    }
    
    [Tags("SettingCluserWithFourWorkers_FreeAllWorkers")]
    [Subject("SettingCluserWithFourWorkers_FreeAllWorkers")]
    public class SettingCluserWithFourWorkers_FreeAllWorkers
    {
        static int resultAddWorkers;
        static int resultFreeWorkers;
        static Message message;
        static Master master;
        static List<Worker> workers;
        static int expectedResult = 4;

        Establish context = () =>
        {
            message = new Message();
            master = new Master(message);
            workers = new List<Worker>();
            for (int i = 1; i < expectedResult + 1; i++)
                workers.Add(new Worker(i, message));
        };

        Because of = () =>
        {
            resultAddWorkers = master.AddWorkers(workers);
            resultFreeWorkers = master.RemoveWorkers(workers);
        };

        It shouldReturnOneWorkerInReservedState = () =>
        {
            resultAddWorkers.ShouldEqual(expectedResult);
            resultFreeWorkers.ShouldEqual(expectedResult);
        };
    }

    [Tags("SettingCluserWithOneWorkersAndTwoMasters_ResultOneMasterPossesedWorkerSecoundGotNothing")]
    [Subject("SettingCluserWithOneWorkersAndTwoMasters_ResultOneMasterPossesedWorkerSecoundGotNothing")]
    public class SettingCluserWithOneWorkersAndTwoMasters_ResultOneMasterPossesedWorkerSecoundGotNothing
    {
        static int result;
        static int result2;
        static Message message;
        static Master master;
        static Master master2;
        static Worker worker;

        Establish context = () =>
        {
            message = new Message();
            master = new Master(message);
            master2 = new Master(message);
            worker = new Worker(1, message);
        };

        Because of = () =>
        {
            result = master.AddWorkers(new List<Worker>() { worker });
            result2 = master2.AddWorkers(new List<Worker>() { worker });
        };

        It shouldReturnOneWorkerInReservedState = () =>
        {
            result.ShouldEqual(1);
            result2.ShouldEqual(0);
        };
    }


    [Tags("SettingCluserWithTwoWorkers_FreeOneWorker")]
    [Subject("SettingCluserWithTwoWorkers_FreeOneWorker")]
    public class SettingCluserWithTwoWorkers_FreeOneWorker
    {
        static int resultAddWorkers;
        static int resultFreeWorkers;
        static Message message;
        static Master master;
        static Worker worker1;
        static Worker worker2;

        Establish context = () =>
        {
            message = new Message();
            master = new Master(message);
            worker1 = new Worker(1, message);
            worker2 = new Worker(2, message);
        };

        Because of = () =>
        {
            resultAddWorkers = master.AddWorkers(new List<Worker>() { worker1, worker2 });
            resultFreeWorkers = master.RemoveWorkers(new List<Worker>() { worker1 });
        };

        It shouldReturnOneWorkerInReservedState = () =>
        {
            resultAddWorkers.ShouldEqual(2);
            resultFreeWorkers.ShouldEqual(1);
        };
    }
}
