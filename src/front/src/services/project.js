import axios from "axios";
const Base_Url ="http://localhost:5001/api/"
export const fetchProjects = async () => {
	try {
		var resposne = await axios.get(Base_Url + "projects");

		return resposne.data;
	} catch (e) {
		console.error(e);
	}
};

export const createProject = async (project) => {
	try {
		var resposne = await axios.post(Base_Url + "projects", project, {
            headers: {
                'Content-Type': 'application/json',
              },
        });

		return resposne.status;
	} catch (e) {
		console.error(e);
	}
};

export const addEmployeeToProject = async (projectId, employeeId) => {
	try {
		var resposne = await axios.post(Base_Url + "projects/add-employee-to-project",{ projectId: projectId, employeeId: employeeId}, {
            headers: {
                'Content-Type': 'application/json',
              },
        });

		return resposne.status;
	} catch (e) {
		console.error(e);
	}
};

export const updateProject = async (id, project) => {
	try {
	  const response = await axios.put(`${Base_Url}projects/`, project, {
		headers: {
		  'Content-Type': 'application/json',
		},
	  });
	  return response.status;
	} catch (error) {
	  console.error(error);
	}
  };

  export const deleteProject = async (id) => {
	try {
	  const response = await axios.delete(`${Base_Url}projects/${id}`, {
		headers: {
		  'Content-Type': 'application/json',
		},
	  });
	  return response.status;
	} catch (error) {
	  console.error(error);
	}
  };
  
  
