import UsersListController from './UsersListController'

export default {
  name : 'usersList',
  config : {
    bindings         : {  users: '<', selected: '<', showDetails: '&onSelected', addUser: '&addUser' },
    templateUrl      : 'src/users/components/list/UsersList.html',
    controller       : [ '$log', UsersListController ]
  }
};
