interface IAction {
  type: string;
}

export interface IContextProps<T> {
  state: T;
  dispatch: ({ type }: IAction) => void;
}
