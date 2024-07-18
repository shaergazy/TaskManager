import ProjectCard from '../components/project';
import { useState, useEffect} from 'react';
import {fetchTasks, updateTask, deleteTask, createTask } from '../services/task'
import {fetchEmployees } from '../services/employee'
import  './project.css'
import { Container, Button, Center,  useToast, Box  } from '@chakra-ui/react';
import ReactPaginate from 'react-paginate';
import ProjectEditModal from '../components/EditProjectModal'; 
import CreateProjectModal from '../components/createProjectModal';
import TaskCard from '../components/Job';
import CreateTaskModal from '../components/CreateTaskModal';
import EditTaskModal from '../components/EditTaskModal';
import { fetchProjects } from '../services/project';
import { useParams } from 'react-router-dom';


export  const Tasks = () =>
{
	const { projectId } = useParams();
	const [Tasks, setTasks] = useState([]);
	const toast = useToast();
	const [employees, setEmployees] = useState();
	const [projects, setProjects] = useState();
	const [currentPage, setCurrentPage] = useState(0);
	const itemsPerPage = 3;
	const [isEditModalOpen, setIsEditModalOpen] = useState(false);
	const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
	const [selectedTask, setSelectedTask] = useState(null);

	useEffect(() => {
		const fetchEmployeesData = async () => {
		  try {
			const employees = await fetchEmployees();
			setEmployees(employees);
		  } catch (error) {
			console.error("Failed to fetch employees:", error);
		  }
		};
	
		fetchEmployeesData();
	  }, []); // [] ensures this effect runs only once

	  useEffect(() => {
		const fetchProjectsData = async () => {
		  try {
			const projects = await fetchProjects();
			setProjects(projects);
		  } catch (error) {
			console.error("Failed to fetch employees:", error);
		  }
		};
	
		fetchProjectsData();
	  }, []); 

	const handlePageClick = ({ selected }) => {
		setCurrentPage(selected);
	  };

	useEffect(() => {
	  const fetchTaskData = async () => {
		try {
		  const Tasks = await fetchTasks(projectId);
		  setTasks(Tasks);
		} catch (error) {
		  console.error("Failed to fetch Projects:", error);
		}
	  };
  
	  fetchTaskData();
	}, []); // [] ensures this effect runs only once

	const onCreateTask = async (task) => {
        try {
          await createTask(task);
          setTasks(prevTasks => [...prevTasks, TaskCard]);
        } catch (error) {
          console.error("Failed to create Task:", error);
        }
      };
    
      const onDelete = async (id) => {
		toast({
			title: 'Item Deleted',
			status: 'success',
			duration: 3000,
			isClosable: true,
		  });
        try {
          await deleteTask(id);
          setTasks(prevTasks => prevTasks.filter(emp => emp.id !== id));
        } catch (error) {
          console.error("Failed to delete Project:", error);
        }
      };

	  const onEdit = (task) => {
		setSelectedTask(task);
		setIsEditModalOpen(true);
	  };

	  const onSaveEdit = async (updatedTask) => {
		await updateTask(updatedTask.id, updatedTask);
		setTasks(Tasks?? "".map(task => task.id === updateTask.id ? updatedTask : task));
	  };

	  const displayTasks = Tasks ?.slice(currentPage * itemsPerPage, (currentPage + 1) * itemsPerPage)
    .map((task) => (
	
      <li key={task.id}>
        <TaskCard
			onEdit={onEdit}
			onDelete={onDelete}
			task={task}
        />
      </li>
    ));

    return(
		<Container maxW="container.lg">
			<Center>
			<Box  borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4} w="full" maxW="md">
		<ul className="flex flex-col gap-5 w-full">
		  {displayTasks}
		</ul>	
		</Box>
			</Center>

  
		<ReactPaginate
		  previousLabel={'Previous'}
		  nextLabel={'Next'}
		  breakLabel={'...'}
		  breakClassName={'break-me'}
		  pageCount={Math.ceil(Tasks?.length / itemsPerPage)}
		  marginPagesDisplayed={2}
		  pageRangeDisplayed={5}
		  onPageChange={handlePageClick}
		  containerClassName={'pagination'}
		  subContainerClassName={'pages pagination'}
		  activeClassName={'active'}
		/>
		<br></br>
        <Center>
			<Button borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4} w="full" maxW="md"  colorScheme="blue" onClick={() => setIsCreateModalOpen(true)}>Create</Button>
		</Center>
		{selectedTask && (
        <EditTaskModal
          isOpen={isEditModalOpen}
		  employees={employees}
		  projects={projects}
          onClose={() => setIsEditModalOpen(false)}
          task={selectedTask}
          onSave={onSaveEdit}
        />
      )}
	  		<CreateTaskModal 
			employees={employees}
			projects={projects}
			isOpen={isCreateModalOpen}
			onClose={() => setIsCreateModalOpen(false)}
			onCreate={onCreateTask}
			/>
	  </Container>
       
    )
}