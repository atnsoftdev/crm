import React, { useContext } from "react";
import { Nav, NavItem, NavLink, Collapse } from "reactstrap";
import { NavLink as RRNavLink } from 'react-router-dom';
import { AppCtx } from "contexts";

export default () => {
  const { state } = useContext(AppCtx);

  return (
    <div
      className={
        "sidebar sidebar-offcanvas " +
        (!state.sidebar.isCollapse ? "active" : "")
      }
    >
      <Nav id="sidebar" vertical>
        <NavItem className="nav-category">Main menu</NavItem>
        <NavItem>
          <NavLink tag={RRNavLink} exact to="/">
            <span className="menu-title">Home</span>
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink tag={RRNavLink} exact to="/leads">
            <span className="menu-title">Leads</span>
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink>
            <span className="menu-title">Contacts</span>
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink>
            <span className="menu-title">Settings</span>
            <i className="menu-arrow" />
          </NavLink>
          <Collapse isOpen={true}>
            <Nav vertical className="sub-menu">
              <NavItem>
                <NavLink href="#">
                  <span className="menu-title">Users</span>
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink>
                  <span className="menu-title">Global Configurations</span>
                </NavLink>
              </NavItem>
            </Nav>
          </Collapse>
        </NavItem>
      </Nav>
    </div>
  );
};
