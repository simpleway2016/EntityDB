using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WayControls.Windows
{
    /// <summary>
    /// ��ȡ�ؼ���Ϣ
    /// </summary>
    public class MessageCutor
    {
        private int preWinProc;
        private IntPtr handler;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public delegate void GetMessageHandler(MessageArgs e);
        /// <summary>
        /// 
        /// </summary>
        public event GetMessageHandler GetMessage;
        /// <summary>
        /// 
        /// </summary>
        public MessageCutor()
        {
            
        }

        private API.MessageCutorHandler cutorhandler;
        /// <summary>
        /// ��ʼ��ȡ
        /// </summary>
        public void Start(IntPtr controlHandler)
        {
            if (cutorhandler == null)
            {
                cutorhandler = new API.MessageCutorHandler(wndProc);
            }
            this.handler = controlHandler;
            //��¼Window Procedure�ĵ�ַ 
            preWinProc = API.GetWindowLong(controlHandler, API.GWL_WNDPROC);
            int hr = API.SetWindowLong(controlHandler, API.GWL_WNDPROC, cutorhandler);
            if (hr < 0 && hr != preWinProc)
                Marshal.ThrowExceptionForHR(hr);
        }

        /// <summary>
        /// ������ȡ
        /// </summary>
        public void Stop()
        {
            //ȡ����Ϣ��ȡ�������ӷ������. 
            API.SetWindowLong(handler, API.GWL_WNDPROC, preWinProc);
        }

        private int wndProc(IntPtr handler, int msg, int wparam, int lparam)
        {
            MessageArgs m = new MessageArgs();
            m.Msg = msg;
            m.WParam = wparam;
            m.LParam = lparam;
            m.Handler = handler;
            if (this.GetMessage != null)
            {
                this.GetMessage(m);
            }
            if(m.CancelBubble == false)
                return API.CallWindowProc(preWinProc, handler, msg, wparam, lparam);
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        ~MessageCutor()
        {
            try
            {
                Stop();
            }
            catch
            {
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public class MessageArgs
    {
        private bool _CancelBubble = false;

        /// <summary>
        /// �Ƿ񲻰���Ϣ�ش����ؼ�
        /// </summary>
        public bool CancelBubble
        {
            get { return _CancelBubble; }
            set { _CancelBubble = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IntPtr Handler;
        /// <summary>
        /// 
        /// </summary>
        public int Msg;
        /// <summary>
        /// 
        /// </summary>
        public int WParam;
        /// <summary>
        /// 
        /// </summary>
        public int LParam;
    }
}
