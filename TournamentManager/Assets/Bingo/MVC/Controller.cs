using UnityEngine;
using System.Collections;

namespace Bingo
{
    public class Controller : Element
    {
        private Model _model;
        protected Model model
        {
            get { return _model ?? (_model = GetComponent<Model>()); }
        }

        private View _view;
        protected View view
        {
            get { return _view ?? (_view = GetComponent<View>()); }
        }

        private Controller _controller;
        protected Controller controller
        {
            get { return _controller ?? (_controller = GetComponent<Controller>()); }
        }
    }

    public class Controller<T> : Controller where T : BaseApplication
    {
        new public T app
        {
            get
            {
                return base.app as T;
            }
        }
    }

    public class NetworkController : NetworkElement
    {
        private Model _model;
        protected Model model
        {
            get { return _model ?? (_model = GetComponent<Model>()); }
        }

        private View _view;
        protected View view
        {
            get { return _view ?? (_view = GetComponent<View>()); }
        }

        private Controller _controller;
        protected Controller controller
        {
            get { return _controller ?? (_controller = GetComponent<Controller>()); }
        }
    }

    public class NetworkController<T> : NetworkController where T : BaseApplication
    {
        new public T app
        {
            get
            {
                return base.app as T;
            }
        }
    }
}
