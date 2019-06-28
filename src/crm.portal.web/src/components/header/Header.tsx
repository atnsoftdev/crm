import React, { useContext } from "react";
import {
  Navbar,
  NavbarBrand,
  Form,
  Nav,
  FormGroup,
  Input,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  NavLink
} from "reactstrap";

import {
  FaSignOutAlt,
  FaInfoCircle,
  FaComment,
  FaEnvelopeOpen,
  FaBell,
  FaBars
} from "react-icons/fa";

import logo from "../../assets/img/logo.svg";
import user from "../../assets/img/user.png";
import { AppCtx, AppActions } from "../../contexts";
import { AuthService } from "services";

const style = {
  userProfile: {
    marginLeft: "32px"
  },
  minWidth: {
    minWidth: "250px"
  }
};

export default () => {
  const { state, dispatch } = useContext(AppCtx);

  return (
    <Navbar className="default-layout col-lg-12 col-12 p-0 fixed-top d-flex flex-row">
      <div className="text-center navbar-brand-wrapper d-flex align-items-top justify-content-center">
        <NavbarBrand className="brand-logo" href="index.html">
          <img src={logo} alt="logo" />{" "}
        </NavbarBrand>
        <NavbarBrand className="brand-logo-mini" href="index.html">
          <img src={logo} alt="logo" />{" "}
        </NavbarBrand>
      </div>
      <div className="navbar-menu-wrapper d-flex align-items-center">
        <Form className="ml-auto search-form d-none d-md-block">
          <FormGroup>
            <Input type="search" placeholder="Search here" />
          </FormGroup>
        </Form>
        <Nav className="ml-auto" navbar>
          <UncontrolledDropdown nav inNavbar>
            <NavLink className="count-indicator">
              <FaBell />
              <span className="count bg-danger">7</span>
            </NavLink>
          </UncontrolledDropdown>

          <UncontrolledDropdown nav inNavbar>
            <NavLink className="count-indicator">
              <FaEnvelopeOpen />
              {state.notification.numberOfMsg > 0 ? (
                <span className="count bg-success">
                  {state.notification.numberOfMsg}
                </span>
              ) : null}
            </NavLink>
          </UncontrolledDropdown>

          <UncontrolledDropdown
            nav
            style={style.userProfile}
            className="d-none d-xl-inline-block user-dropdown"
          >
            <DropdownToggle nav caret>
              <img className="img-xs rounded-circle" src={user} alt="Profile" />
            </DropdownToggle>
            <DropdownMenu right>
              <div className="dropdown-header text-center">
                <p className="mb-1 mt-3 font-weight-semibold">
                  {state.userLogin ? state.userLogin.userName : ""}
                </p>
                <p className="font-weight-light text-muted mb-0">
                  {state.userLogin ? state.userLogin.email : ""}
                </p>
              </div>
              <DropdownItem>
                <FaInfoCircle className="mr-2" />
                My Profile
              </DropdownItem>
              <DropdownItem>
                <FaComment className="mr-2" />
                Messages
              </DropdownItem>
              <DropdownItem divider />
              <DropdownItem
                onClick={() => {
                  AuthService.signOut();
                }}
              >
                <FaSignOutAlt className="mr-2" /> Sign Out
                <i className="dropdown-item-icon ti-power-off" />
              </DropdownItem>
            </DropdownMenu>
          </UncontrolledDropdown>
        </Nav>
        <button
          className="navbar-toggler navbar-toggler-right d-lg-none align-self-center"
          type="button"
          data-toggle="offcanvas"
          onClick={() => {
            dispatch(AppActions.toggleCollapse());
          }}
        >
          <FaBars className="ml-2" />
        </button>
      </div>
    </Navbar>
  );
};
