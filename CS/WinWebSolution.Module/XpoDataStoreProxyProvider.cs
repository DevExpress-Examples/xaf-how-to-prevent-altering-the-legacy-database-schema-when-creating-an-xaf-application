using System;
using DevExpress.ExpressApp;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.Xpo;

namespace WinWebSolution.Module {
    public class XpoDataStoreProxyProvider : IXpoDataStoreProvider {
        private XpoDataStoreProxy proxy;
        public XpoDataStoreProxyProvider() {
            proxy = new XpoDataStoreProxy();
        }
        public string ConnectionString {
            get { return ""; }
        }
        public DevExpress.Xpo.DB.IDataStore CreateUpdatingStore(out IDisposable[] disposableObjects) {
            disposableObjects = null;
            return proxy;
        }
        public DevExpress.Xpo.DB.IDataStore CreateWorkingStore(out IDisposable[] disposableObjects) {
            disposableObjects = null;
            return proxy;
        }
        public XPDictionary XPDictionary {
            get { return null; }
        }
        private bool isInitialized;
        public bool IsInitialized {
            get {
                return isInitialized;
            }
        }
        public void Initialize(XPDictionary dictionary, string legacyConnectionString, string tempConnectionString) {
            proxy.Initialize(dictionary, legacyConnectionString, tempConnectionString);
            isInitialized = true;
        }
    }
}
