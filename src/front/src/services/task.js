import axios from "axios";

const Base_Url = "http://localhost:5001/api/";

export const fetchTasks = async (projectId = null) => {
  try {
    let url = Base_Url + "jobs";
    if (projectId) {
      url += `?projectId=${projectId}`;
    }

    const response = await axios.get(url);
    return response.data;
  } catch (error) {
    console.error(error);
    throw error; // Передаем ошибку наверх для обработки в компонентах, если это необходимо
  }
};
export const createTask = async (task) => {
	try {
		var resposne = await axios.post(Base_Url + "jobs", task, {
            headers: {
                'Content-Type': 'application/json',
              },
        });

		return resposne.status;
	} catch (e) {
		console.error(e);
	}
};

export const updateTask = async (id, task) => {
	try {
	  const response = await axios.put(`${Base_Url}jobs/`, task, {
		headers: {
		  'Content-Type': 'application/json',
		},
	  });
	  return response.status;
	} catch (error) {
	  console.error(error);
	}
  };

  export const deleteTask = async (id) => {
	try {
	  const response = await axios.delete(`${Base_Url}jobs/${id}`, {
		headers: {
		  'Content-Type': 'application/json',
		},
	  });
	  return response.status;
	} catch (error) {
	  console.error(error);
	}
  };
  
  
