/**
 * Users DataService
 * Uses embedded, hard-coded data model; acts asynchronously to simulate
 * remote data service call(s).
 *
 * @returns {{loadAll: Function}}
 * @constructor
 */
function UsersDataService($http, $q) {
  var baseApiUrl = 'http://localhost:50536/api/Users/';

  function loadAllUsers() {
    return $http.get(baseApiUrl);
  }

  function updateUser(user) {
    return $http.put(baseApiUrl + user.id, user);
  }

  function createUser(user) {
    return $http.post(baseApiUrl, user);
  }

  function deleteUser(user) {
    if (user.id) {
      return $http.delete(baseApiUrl + user.id);
    }
    else {
      return $q.when(null);
    }
  }

  return {
    loadAllUsers: loadAllUsers,
    updateUser: updateUser,
    createUser: createUser,
    deleteUser: deleteUser
  };
}

export default ['$http', '$q', UsersDataService];

