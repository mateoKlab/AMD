using UnityEngine;
using System.Collections;

namespace Bingo
{
    public class Model : Element
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

    public class Model<T> : Model where T : BaseApplication
    {
        new public T app
        {
            get
            {
                return base.app as T;
            }
        }
    }

    public class NetworkModel : NetworkElement
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

    public class NetworkModel<T> : NetworkModel where T : BaseApplication
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
