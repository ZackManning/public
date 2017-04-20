/**
 * Users DataService
 * Uses embedded, hard-coded data model; acts asynchronously to simulate
 * remote data service call(s).
 *
 * @returns {{loadAll: Function}}
 * @constructor
 */
function UsersDataService($q) {
  var users = [
    {
      name: 'Juan Jaspe',
      githubHandle: 'jjaspenextech',
      address: '5550 Executive Dr',
      city: 'Tampa',
      state: 'FL',
      zip: '33609'
    },
    {
      name: 'Brandon Ripley',
      githubHandle: 'bripley-nxtech'
    },
    {
      name: 'Brad Savon',
      githubHandle: 'bradsavon'
    },
    {
      name: 'Zack Manning',
      githubHandle: 'ZackManning'
    }
  ];

  function loadAllUsers() {
    return $q.when(users);
  }

  function saveUser(user) {
    return $q(function (resolve, reject) {
      resolve(user);
    });
  }

  return {
    loadAllUsers: loadAllUsers,
    saveUser: saveUser
  };
}

export default ['$q', UsersDataService];

