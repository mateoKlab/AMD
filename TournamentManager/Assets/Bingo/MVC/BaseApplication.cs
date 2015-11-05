using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Bingo
{
    public class BaseApplication : Element
    {
        private static List<string> _args;
        protected static List<string> args
        {
            get
            {
                return _args?? (_args = new List<string>());
            }
        }

        static BaseApplication()
        {

        }

        private Model _model;
        public Model model
        {
            get
            {
                return _model = Inject(_model);
            }
        }

        private View _view;
        public View view
        {
            get
            {
                return _view = Inject(_view);
            }
        }

        private Controller _controller;
        public Controller controller
        {
            get
            {
                return _controller = Inject(_controller);
            }
        }
    }

    public class BaseApplication<M, V, C> : BaseApplication
        where M : Element
        where V : Element
        where C : Element
    {
        new public M model
        {
            get
            {
                return base.model as M;
            }
        }

        new public V view
        {
            get
            {
                return base.view as V;
            }
        }

        new public C controller
        {
            get
            {
                return base.controller as C;
            }
        }
    }
}
