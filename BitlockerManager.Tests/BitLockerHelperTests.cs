namespace SunamoBitLocker.Tests
{
    [TestClass]
    public class BitLockerHelperTests
    {
        const string f = @"E:\_Test\sunamoWithoutLocalDep\SunamoBitLocker\a.txt";
        const string ab = "ab";

        [TestMethod]
        public void PassIfDriveLocked()
        {
            TF.IsFolderLockedByBitLocker = BitLockerHelper.IsFolderLockedByBitLocker;
            BitLockerHelper.Init();

            TF.WriteAllText(f, ab);
            Assert.IsNull(TF.ReadAllText(f));
        }

        [TestMethod]
        public void PassIfDriveUnLocked()
        {
            TF.IsFolderLockedByBitLocker = BitLockerHelper.IsFolderLockedByBitLocker;
            BitLockerHelper.Init();

            TF.WriteAllText(f, ab);
            Assert.AreEqual(ab, TF.ReadAllText(f));
        }
    }
}
