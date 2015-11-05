using UnityEngine;
using System.Collections;

namespace Bingo
{
    public class View : Element
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

    public class View<T> : View where T : BaseApplication
    {
        new public T app
        {
            get
            {
                return base.app as T;
            }
        }
    }

    public class NetworkView : NetworkElement
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

    public class NetworkView<T> : NetworkView where T : BaseApplication
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
