import React, { useReducer, Reducer } from "react";

import { IAction, IContextProps } from "./ContextProps";

export const TOGGLE_COLLAPSE = 'TOGGLE_COLLAPSE';

interface Sidebar {
  isCollapse: boolean;
}

interface UIState {
  sidebar: Sidebar;
  notification: any;
  isAuthenticated: boolean;
  userLogin: any;
}

const initialState: UIState = {
  sidebar: {
    isCollapse: true
  },
  notification: {
    numberOfMsg: 10
  },
  isAuthenticated: true,
  userLogin: {
    userName: "user1",
    email: "user1@nomail.com"
  }
};

const AppCtx = React.createContext({} as IContextProps<UIState>);

const reducer = (state: UIState, action: IAction) => {
  switch (action.type) {
    case TOGGLE_COLLAPSE:
      return {
        ...state,
        sidebar: {
          isCollapse: !state.sidebar.isCollapse
        }
      };
    default:
      return initialState;
  }
};

const AppCtxProvider = (props: any) => {
  let [state, dispatch] = useReducer<Reducer<UIState, any>>(
    reducer,
    initialState
  );
  let value = { state, dispatch };

  return <AppCtx.Provider value={value}>{props.children}</AppCtx.Provider>;
};

export { AppCtx, AppCtxProvider };
