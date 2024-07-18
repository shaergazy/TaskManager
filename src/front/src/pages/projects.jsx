import ProjectCard from '../components/project';
import { useState, useEffect} from 'react';
import {fetchProjects, updateProject, deleteProject, createProject, addEmployeeToProject } from '../services/project'
import {fetchEmployees } from '../services/employee'
import  './project.css'
import { Container, Button, Center,  useToast, Box  } from '@chakra-ui/react';
import ReactPaginate from 'react-paginate';
import ProjectEditModal from '../components/EditProjectModal'; 
import CreateProjectModal from '../components/createProjectModal';
import AddEmployeeToProjectModal from '../components/AddEmployeeToProject';
import { useParams } from 'react-router-dom';


export default function Projects () 
{
	const { projectId } = useParams();
	const [Projects, setProjects] = useState([]);
	const toast = useToast();
	const [employees, setEmployees] = useState();
	const [currentPage, setCurrentPage] = useState(0);
	const itemsPerPage = 3;
	const [isEditModalOpen, setIsEditModalOpen] = useState(false);
	const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
	const [isAddEmployeeModalOpen, setIsAddEmployeeModalOpen] = useState(false);
	const [selectedProject, setSelectedProject] = useState(null);

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

	const handlePageClick = ({ selected }) => {
		setCurrentPage(selected);
	  };

	useEffect(() => {
	  const fetchProjectsData = async () => {
		try {
		  const Projects = await fetchProjects();
		  setProjects(Projects);
		} catch (error) {
		  console.error("Failed to fetch Projects:", error);
		}
	  };
  
	  fetchProjectsData();
	}, []); 

	const onCreateProject = async (project) => {
        try {
          await createProject(project);
          setProjects(prevProjects => [...prevProjects, ProjectCard]);
        } catch (error) {
          console.error("Failed to create Project:", error);
        }
      };

	  const onAddEmployee = async (projectId,selectedEmployeeId) => {
        try {
          await addEmployeeToProject(projectId, selectedEmployeeId);
          setProjects(prevProjects => [...prevProjects, ProjectCard]);
        } catch (error) {
          console.error("Failed to create Project:", error);
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
          await deleteProject(id);
          setProjects(prevProjects => prevProjects.filter(emp => emp.id !== id));
        } catch (error) {
          console.error("Failed to delete Project:", error);
        }
      };

	  const handleAddEmployee = (project) => {
		setSelectedProject(project);
		setIsAddEmployeeModalOpen(true);
	  };


	  const onEdit = (project) => {
		setSelectedProject(project);
		setIsEditModalOpen(true);
	  };

	  const onSaveEdit = async (updatedProject) => {
		await updateProject(updatedProject.id, updatedProject);
		setProjects(projects?? "".map(project => project.id === updatedProject.id ? updatedProject : project));
	  };

	  const displayProjects = Projects ?.slice(currentPage * itemsPerPage, (currentPage + 1) * itemsPerPage)
    .map((project) => (
	
      <li key={project.id}>
        <ProjectCard
			onEdit={onEdit}
			onDelete={onDelete}
			project={project}
			onAddEmployee={handleAddEmployee}
        />
      </li>
    ));

    return(
		<Container maxW="container.lg">
			<Center>
			<Box  borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4} w="full" maxW="md">
		<ul className="flex flex-col gap-5 w-full">
		  {displayProjects}
		</ul>	
		</Box>
			</Center>

  
		<ReactPaginate
		  previousLabel={'Previous'}
		  nextLabel={'Next'}
		  breakLabel={'...'}
		  breakClassName={'break-me'}
		  pageCount={Math.ceil(Projects.length / itemsPerPage)}
		  marginPagesDisplayed={2}
		  pageRangeDisplayed={5}
		  onPageChange={handlePageClick}
		  containerClassName={'pagination'}
		  subContainerClassName={'pages pagination'}
		  activeClassName={'active'}
		/>
		<br></br>
        <Center><Button borderWidth="1px" borderRadius="lg" overflow="hidden" p={4} mb={4} w="full" maxW="md"  colorScheme="blue" onClick={() => setIsCreateModalOpen(true)}>Create</Button></Center>
		{selectedProject && (
        <ProjectEditModal
          isOpen={isEditModalOpen}
          onClose={() => setIsEditModalOpen(false)}
          project={selectedProject}
          onSave={onSaveEdit}
        />
      )}
		<AddEmployeeToProjectModal 
		employees={employees}
		isOpen={isAddEmployeeModalOpen}
		onClose = {() => setIsAddEmployeeModalOpen(false)}
		projectId={selectedProject?.id}
		onAddEmployee ={onAddEmployee}/>

	  		<CreateProjectModal 
			employees={employees}
			isOpen={isCreateModalOpen}
			onClose={() => setIsCreateModalOpen(false)}
			onCreate={onCreateProject}
			/>
	  </Container>
       
    )
}