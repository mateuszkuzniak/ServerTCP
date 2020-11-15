using System;

namespace FileLibrary
{
    interface FileInterface
    {
        string open();
        string close();
        void created();
        void edit();
    }
}
